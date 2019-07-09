using Moq;
using MyBlog.Services;
using MyBlogWeb.Controllers;
using Xunit;

namespace MyBlog.Test
{
    public class UnitTest1
    {
        private readonly ValuesController valuesController;
        public UnitTest1()
        {
            var testapi = new Mock<ITestApiService>();
            testapi.Setup(a => a.Get(1)).Returns(new Models.TestApi { Id = 1 });
           valuesController = new ValuesController(null, testapi.Object);
        }
        [Fact]
        public void Test1()
        {
            var res=valuesController.Get(1);
            Assert.Equal(1, res);
        }
    }
}
