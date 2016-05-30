using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stubsharp.Utility;
using Xunit;

namespace Stubsharp.Tests.Utility
{
    public class TokenBuilderTests
    {
        [Fact]
        public void Creates_Basic_Authorization_Token()
        {
            string key = "xyzpKNbQOmfBO1vptfefJEOAa";
            string secret = "abcGSD7rFkSCY9FCfU0n95GTdfgUU";

            var result = TokenBuilder.CreateAuthorizationToken(key, secret);

            Assert.Equal("eHl6cEtOYlFPbWZCTzF2cHRmZWZKRU9BYTphYmNHU0Q3ckZrU0NZOUZDZlUwbjk1R1RkZmdVVQ==", result);
        }
    }
}
