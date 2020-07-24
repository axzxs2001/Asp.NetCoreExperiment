using ClassLibrary1;
using System;
using Xunit;

namespace MyFirstUnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void PassingTest()
        {
            var cl1 = new Class1();
            Assert.Equal(4, cl1.Add(2, 2));
        }

        [Fact]
        public void FailingTest()
        {
            var cl1 = new Class1();
            Assert.Equal(5, cl1.Add(2, 3));
        }
   
    }
}
