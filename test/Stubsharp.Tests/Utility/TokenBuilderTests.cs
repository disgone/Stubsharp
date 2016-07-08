using System;
using Stubsharp.Utility;
using Xunit;
using Xunit.Abstractions;

namespace Stubsharp.Tests.Utility
{
    public class TokenBuilderTests
    {
        private ITestOutputHelper  Output { get; set; }

        public TokenBuilderTests(ITestOutputHelper output)
        {
            Output = output;
        }

        [Fact]
        public void Creates_Basic_Authorization_Token()
        {
            string key = "xyzpKNbQOmfBO1vptfefJEOAa";
            string secret = "abcGSD7rFkSCY9FCfU0n95GTdfgUU";

            var result = TokenBuilder.CreateAuthorizationToken(key, secret);

            Assert.Equal("eHl6cEtOYlFPbWZCTzF2cHRmZWZKRU9BYTphYmNHU0Q3ckZrU0NZOUZDZlUwbjk1R1RkZmdVVQ==", result);
        }

        [Fact]
        public void Given_Null_Key_Throws_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => TokenBuilder.CreateAuthorizationToken(null, "secret"));
        }

        [Fact]
        public void Given_Null_Secret_Throws_Exception()
        {
            Assert.Throws<ArgumentNullException>(() => TokenBuilder.CreateAuthorizationToken("apikey", null));
        }
    }
}
