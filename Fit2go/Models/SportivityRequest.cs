using Fit2go.Options;
using Fit2go.Utils;

namespace Fit2go.Models
{
    public class SportivityRequest
    {
        public SportivityRequest(UserOption user)
        {
            User = user.User;
            Password = user.Password;
            Hash = SportivityHashUtil.GetSha1($"moshi moshi {user} desu");
        }

        protected SportivityRequest() { }

        public string User { get; set; }
        public string Password { get; set; }
        public string Hash { get; protected set; }
    }
}
