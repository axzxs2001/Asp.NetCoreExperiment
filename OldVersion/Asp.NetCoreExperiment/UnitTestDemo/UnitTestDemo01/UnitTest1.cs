using System;
using TestProject01;
using Xunit;

namespace UnitTestDemo01
{
    public class TestTestClass
    {
        [Fact]
        public void AddTest()
        {
            var test = new TestClass();
            var result = test.Add(1, 2);
            Assert.Equal(3, result);
        }
        [Theory]
        [InlineData(-1)]
        [InlineData(2)]
        [InlineData(3)]
        public void GetTest(int v)
        {
            var test = new TestClass();
            var result = test.Get(v);

            Assert.Equal(v<0?false:(v%2==0),result);
        }
    }
}
