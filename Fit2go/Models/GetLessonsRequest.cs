using System;
using System.Text.Json.Serialization;
using Fit2go.Converters;
using Fit2go.Options;

namespace Fit2go.Models
{
    public class GetLessonsRequest : SportivityRequest
    {
        public GetLessonsRequest(UserOption user, DateTimeOffset startDate, DateTimeOffset endDate, int locationId)
            : base(user)
        {
            StartDate = startDate;
            EndDate = endDate;
            LocationId = locationId;
        }

        public DateTimeOffset StartDate { get; }
        public DateTimeOffset EndDate { get; }

        [JsonConverter(typeof(SportivityIntConverter))]
        public int LocationId { get; }
    }
}
