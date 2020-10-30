using System;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Fit2go.Models;
using Fit2go.Options;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Fit2go.Clients
{
    public class SendgridClient
    {
        private static readonly CultureInfo s_dutchCulture = CultureInfo.GetCultureInfo("nl-NL");
        private readonly SendGridClient _client;
        private readonly EmailOptions _options;

        public SendgridClient(HttpClient httpClient, IOptionsMonitor<EmailOptions> options)
        {
            _options = options.CurrentValue;
            _client = new SendGridClient(httpClient, _options.SendgridApiKey);
        }

        public async Task SendJoinMail(SendJoinMailRequest request, CancellationToken cancellationToken = default)
        {
            const string subject = "Successvol automatisch ingeschreven Fit2Go";
            string content = CreateMailContent(request);

            await SendMail(request.User, subject, content, cancellationToken);
        }

        public async Task SendFailedMail(UserOption user, CancellationToken cancellationToken = default)
        {
            const string subject = "Niet automatisch ingeschreven Fit2Go";
            const string content = "Er ging iets mis met het automatisch inschrijven bij Fit2Go.";

            await SendMail(user, subject, content, cancellationToken);
        }

        private async Task SendMail(UserOption user, string subject, string content, CancellationToken cancellationToken = default)
        {
            EmailAddress from = CreateFromEmail();
            EmailAddress to = CreateToEmail(user);
            SendGridMessage message = MailHelper.CreateSingleEmail(from, to, subject, content, string.Empty);

            await _client.SendEmailAsync(message, cancellationToken);
        }

        private EmailAddress CreateFromEmail() =>
            new EmailAddress(_options.EmailFrom, _options.NameFrom);

        private static EmailAddress CreateToEmail(UserOption user) =>
            new EmailAddress(user.User);

        private static string CreateMailContent(SendJoinMailRequest request)
        {
            var builder = new StringBuilder();
            if (request.Success.Count > 0)
            {
                builder.AppendFormat("Successvol ingeschreven voor de volgende lessen:{0}", Environment.NewLine);
                builder.AppendFormat("{0}{1}", string.Join(Environment.NewLine, request.Success.Select(l => l.LessonStartTime.ToString("g", s_dutchCulture))), Environment.NewLine);
                builder.Append(Environment.NewLine);
            }

            if (request.Failed.Count > 0)
            {
                builder.AppendFormat("Kon niet inschrijven voor de volgende lessen:{0}", Environment.NewLine);
                builder.AppendFormat("{0}{1}", string.Join(Environment.NewLine, request.Failed.Select(l => l.LessonStartTime.ToString("g", s_dutchCulture))), Environment.NewLine);
                builder.Append(Environment.NewLine);
            }

            return builder.ToString();
        }
    }
}
