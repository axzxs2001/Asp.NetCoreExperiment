using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace WebDemo01.UnitTest
{
    public class UnitTest2
    {
        [Fact]
        public void Execute_Test_ReturnTrue()
        {
            var mock = new Mock<IMyInterface>();
            mock.SetupSequence(s => s.Exe(It.IsAny<int>())).Returns(false).Returns(true);
            // mock.Setup(s => s.Exe(It.IsAny<int>())).Returns(true);
            var usemyclass = new UseMyClass(mock.Object);
            var result = usemyclass.F(1);
            Assert.True(result);
        }
    }

    public class UseMyClass
    {
        readonly IMyInterface _myInterface;
        public UseMyClass(IMyInterface myInterface)
        {
            _myInterface = myInterface;
        }
        public bool F(int i)
        {
            var r1 = _myInterface.Exe(i);
            var r2 = _myInterface.Exe(i);
            return r2 && !r1;
        }
    }
    public interface IMyInterface
    {
        bool Exe(int i);

    }
    public class MyClass : IMyInterface
    {
        public bool Exe(int i)
        {
            return i > 1;
        }
    }
}
