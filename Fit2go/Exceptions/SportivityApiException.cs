using System;
using System.Net;

namespace Fit2go.Exceptions
{
    public class SportivityApiException : Exception
    {
        public HttpStatusCode Status { get; }
        public string? Content { get; }

        public SportivityApiException(HttpStatusCode status, string? content)
            : base($"Sportivity API call failed, status {status}, content {content}")
        {
            Status = status;
            Content = content;
        }
    }
}
