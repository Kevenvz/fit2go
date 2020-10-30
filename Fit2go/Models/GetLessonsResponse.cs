using System.Collections.Generic;

namespace Fit2go.Models
{
    public class GetLessonsResponse
    {
        public IEnumerable<Lesson> Lessons { get; set; }
    }
}
