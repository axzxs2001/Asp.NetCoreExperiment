using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace WebDemo01.UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
          
            var mock = new Mock<IDemoService>();
            mock.Setup(demo => demo.Method()).Returns("gsw");
            Assert.Equal("gsw", mock.Object.Method());
        }
    }
    public interface IDemoService
    {
        string Method();
    }   
}
