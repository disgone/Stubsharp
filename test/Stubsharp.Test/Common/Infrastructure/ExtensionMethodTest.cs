using System;
using System.Collections.Generic;
using Stubsharp.Common.Infrastructure;
using Xunit;

namespace Stubsharp.Test.Common.Infrastructure
{
    public static class ExtensionMethodTest
    {
        public class StringTests
        {
            [Theory]
            [InlineData("my value", true)]
            [InlineData("   ", false)]
            [InlineData(null, false)]
            public void HasValueDetectsStringValues(string str, bool expected)
            {
                Assert.Equal(expected, str.HasValue());
            }

            [Fact]
            public void ToBase64EncodesString()
            {

                Assert.Equal("bXkgc3RyaW5nIHZhbHVlcyBhcmUgZ3JlYXQgLS0gdGhlIGJlc3Q=", "my string values are great -- the best".ToBase64());
            }

            [Fact]
            public void FromBase64EDecodesString()
            {
                Assert.Equal("my string values are great -- the best", "bXkgc3RyaW5nIHZhbHVlcyBhcmUgZ3JlYXQgLS0gdGhlIGJlc3Q=".FromBase64());
            }

            [Fact]
            public void ApplyParametersAddsUrlParameters()
            {
                var url = new Uri("https://www.example.com/");

                url = url.ApplyParameters(new Dictionary<string, string>
                {
                    {"sort", "desc"}
                });

                Assert.Equal("https://www.example.com/?sort=desc", url.ToString());
            }

            [Fact]
            public void ApplyParametersEncodesParameters()
            {
                var url = new Uri("https://www.example.com");

                url = url.ApplyParameters(new Dictionary<string, string>
                {
                    {"sort", "zone&desc"}
                });

                Assert.Equal("https://www.example.com/?sort=zone%26desc", url.ToString());
            }

            [Fact]
            public void ApplyParametersUpdatesExistingParameter()
            {
                var url = new Uri("https://www.example.com?q=term&sort=asc");

                url = url.ApplyParameters(new Dictionary<string, string>
                {
                    {"sort", "desc"}
                });

                Assert.Equal("https://www.example.com/?q=term&sort=desc", url.ToString());
            }

            [Fact]
            public void ApplyParametersRemovesExistingParameter()
            {
                var url = new Uri("https://www.example.com?q=term&sort=asc");

                url = url.ApplyParameters(new Dictionary<string, string>
                {
                    {"q", null}
                });

                Assert.Equal("https://www.example.com/?sort=asc", url.ToString());
            }

            [Fact]
            public void ApplyParametersHandlesRelativeUrl()
            {
                var url = new Uri("links.html", UriKind.Relative);

                url = url.ApplyParameters(new Dictionary<string, string>
                {
                    {"sort", "zone&desc"}
                });

                Assert.Contains("links.html?sort=zone%26desc", url.ToString());

                Assert.True(!url.IsAbsoluteUri);
            }

            [Fact]
            public void ApplyParametersHandlesEmptyParameters()
            {
                var url = new Uri("https://www.example.com");

                var result = url.ApplyParameters(new Dictionary<string, string>());

                Assert.Equal(url, result);
            }

            [Fact]
            public void RemoveParameterRemovesParameter()
            {
                var url = new Uri("https://www.example.com?q=term&sort=asc");

                url = url.RemoveParameter("q");

                Assert.Equal("https://www.example.com/?sort=asc", url.ToString());
            }
        }

        public class TimespanTests
        {
            [Fact]
            public void SecondsReturnsExpectedTimespan()
            {
                TimeSpan expected = new TimeSpan(0, 0, 5);

                Assert.Equal(expected, 5.Seconds());
            }
        }

        public class CollectionTests
        {
            [Fact]
            public void SetOrUpdateAddsNewKey()
            {
                var collection = new Dictionary<string, string>
                {
                    {"existing", "value"}
                };

                collection.SetOrUpdate("newbie", "o_O");

                Assert.Contains(collection, n => n.Key == "newbie" && n.Value == "o_O");
            }

            [Fact]
            public void SetOrUpdateModifiesExistingValue()
            {
                var collection = new Dictionary<string, string>
                {
                    {"existing", "value"}
                };

                collection.SetOrUpdate("existing", "newvalue");

                Assert.Contains(collection, n => n.Key == "existing" && n.Value == "newvalue");
            }

            [Fact]
            public void SetOrUpdateRemovesKeyWhenNewValueIsNull()
            {
                var collection = new Dictionary<string, string>
                {
                    {"existing", "value"}
                };

                collection.SetOrUpdate("existing", null);

                Assert.False(collection.ContainsKey("existing"));
            }

            [Fact]
            public void SetOrUpdateSkipsMissingKey()
            {
                var collection = new Dictionary<string, string>
                {
                    {"existing", "value"}
                };

                collection.SetOrUpdate("trouble", null);

                Assert.False(collection.ContainsKey("trouble"));
            }
        }
    }
}
