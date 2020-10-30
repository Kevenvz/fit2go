using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fit2go.Clients;
using Fit2go.Models;
using Fit2go.Options;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Fit2go
{
    public class JoinLesson
    {
        private const string FunctionName = nameof(JoinLesson);
        private const string TimerExpression = "0 0 2 * * 1-5";

        private readonly SportivityClient _client;
        private readonly SportivityOptions _options;
        private readonly SendgridClient _emailClient;

        public JoinLesson(SportivityClient client, IOptionsMonitor<SportivityOptions> options, SendgridClient emailClient)
        {
            _client = client;
            _options = options.CurrentValue;
            _emailClient = emailClient;
        }

        [FunctionName(FunctionName)]
        public async Task Run(
            [TimerTrigger(TimerExpression)] TimerInfo myTimer,
            ILogger log)
        {
            DateTimeOffset today = DateTimeOffset.UtcNow.Date.AddHours(1); // No clue why I need to do .AddHours(1)
            DateTimeOffset[] lessonTimes = new[] {
                CreateLessonDate(today, 18),
                CreateLessonDate(today, 19)
            };

            IEnumerable<Task> tasks = _options.Users.Select(u => ProcessUser(u, today, lessonTimes, log));
            await Task.WhenAll(tasks);

            log.LogInformation("Done with {FunctionName}", FunctionName);
        }

        private async Task ProcessUser(
            UserOption user,
            DateTimeOffset date,
            IEnumerable<DateTimeOffset> lessonTimes,
            ILogger log,
            CancellationToken cancellationToken = default)
        {
            var lessonRequest = new GetLessonsRequest(user, date, date.AddDays(1), _options.LocationId);
            GetLessonsResponse lessonResponse = await _client.GetLessons(lessonRequest, cancellationToken);

            List<Lesson> lessonsToJoin = lessonResponse.Lessons
                // Use OrdinalIgnoreCase and keyword "vrij" since the descriptions aren't consistent
                .Where(l => l.Description.Contains("vrij", StringComparison.OrdinalIgnoreCase)
                            && lessonTimes.Contains(l.LessonStartTime))
                .ToList();

            if (lessonsToJoin.Count > 0)
            {
                var success = new List<Lesson>();
                var failed = new List<Lesson>();

                foreach (Lesson lesson in lessonsToJoin)
                {
                    try
                    {
                        var joinRequest = new JoinLessonRequest(user, lesson.Id);
                        JoinLessonResponse joinResponse = await _client.JoinLesson(joinRequest, cancellationToken);

                        if (!joinResponse.Warning)
                        {
                            success.Add(lesson);
                        }
                        else
                        {
                            failed.Add(lesson);
                        }
                    }
                    catch (Exception ex)
                    {
                        log.LogError(ex, "Something went wrong while joining a lesson. {User} {LessonId}", user.User, lesson.Id);
                        failed.Add(lesson);
                    }
                }

                if (success.Count > 0)
                {
                    var mailRequest = new SendJoinMailRequest(user, success, failed);
                    await _emailClient.SendJoinMail(mailRequest, cancellationToken);
                    return;
                }
            }
            else
            {
                log.LogWarning("LessonsToJoin is empty for {User}", user.User);
            }

            await _emailClient.SendFailedMail(user);
        }

        private static DateTimeOffset CreateLessonDate(DateTimeOffset today, int hour) =>
            new DateTimeOffset(today.Year, today.Month, today.Day, hour, 0, 0, TimeSpan.Zero);
    }
}
