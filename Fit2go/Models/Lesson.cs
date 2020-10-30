using System;
using System.Text.Json.Serialization;

namespace Fit2go.Models
{
    public class Lesson
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        public string Description { get; set; }
        public string LocationName { get; set; }
        public DateTimeOffset LessonStartTime { get; set; }
        public DateTimeOffset LessonEndTime { get; set; }
    }
}
