using Fit2go.Options;
using Fit2go.Utils;

namespace Fit2go.Models
{
    public abstract class SportivityRequest
    {
        protected SportivityRequest(UserOption user)
        {
            User = user.User;
            Password = user.Password;
            Hash = SportivityHashUtil.GetSha1($"moshi moshi {user.User} desu");
        }

        public string User { get; }
        public string Password { get; }
        public string Hash { get; }
    }
}
