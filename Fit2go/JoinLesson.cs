using System;
using Fit2go.Clients;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace Fit2go
{
    public class JoinLesson
    {
        private const string FunctionName = nameof(JoinLesson);
        private const string TimerExpression = "0 0 4 * * *";

        private readonly SportivityClient _client;

        public JoinLesson(SportivityClient client)
        {
            _client = client;
        }

        [FunctionName(FunctionName)]
        public void Run([TimerTrigger(TimerExpression)] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
