using System.Collections.Generic;
using Fit2go.Options;

namespace Fit2go.Models
{
    public class SendJoinMailRequest
    {
        public SendJoinMailRequest(UserOption user, ICollection<Lesson> success, ICollection<Lesson> failed)
        {
            User = user;
            Success = success;
            Failed = failed;
        }

        public UserOption User { get; set; }
        public ICollection<Lesson> Success { get; set; }
        public ICollection<Lesson> Failed { get; set; }
    }
}
