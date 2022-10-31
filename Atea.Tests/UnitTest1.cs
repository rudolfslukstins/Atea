using Atea.Services;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Refit;

namespace Atea.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async void TestMethod1()
        {
            var apiResponse = RestService.For<IPublicApi>("https://api.publicapis.org");
            var octocat = await apiResponse.GetApi();
            octocat.Should().NotBeNull();
        }
    }
}
