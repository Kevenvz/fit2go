using Fit2go.Utils;
using Xunit;

namespace Fit2Go.Tests.Utils
{
    public class SportivityHashUtilTests
    {
        [Theory]
        [InlineData("moshi moshi username desu", "26cac08cdf0c8510747b1bda2396ed192e473bab")]
        public void GetSha1_CreatesCorrectHash(string input, string hash)
        {
            // Act
            string result = SportivityHashUtil.GetSha1(input);

            // Assert
            Assert.Equal(hash, result);
        }
    }
}
