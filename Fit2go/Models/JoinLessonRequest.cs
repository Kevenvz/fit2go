using Fit2go.Options;

namespace Fit2go.Models
{
    public class JoinLessonRequest : SportivityRequest
    {
        public JoinLessonRequest(UserOption user, int lessonId) : base(user)
        {
            LessonId = lessonId;
        }

        public int LessonId { get; set; }
    }
}
