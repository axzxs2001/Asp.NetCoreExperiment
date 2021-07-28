using Castle.Components.DictionaryAdapter;
using Moq;
using System;
using System.Threading.Tasks;
using Telerik.JustMock;
using Telerik.JustMock.Expectations.Abstraction;
using WebDemo01.Services;
using Xunit;

namespace WebDemo01.UnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void Execute_Test_ReturnTrue()
        {
            var sql = "";
            var mock = new Mock<IDapperPlusWrite>();
            mock.Setup(demo => demo.Execute(It.IsAny<string>(), It.IsAny<object>(), null, null, null)).Returns(1);
            Assert.Equal(1, mock.Object.Execute(sql));
        }

        [Fact]
        public void Test1()
        {
            //Arrange 
            var b = Telerik.JustMock.Mock.Create<B>();      
            Telerik.JustMock.Mock.Arrange(() => b.FF()).Returns("abc");
     
            //Act 

            var a = new A(b);
            var r = a.Call_B_FF();

            //Assert 
            Assert.Equal(r, "aabc");
        }
    }



    public class A
    {
        B _b;
        public A(B b)
        {
            _b = b;
        }
        public string Call_B_FF()
        {
           // var b = new B();
            return "a" + _b.FF();
        }
    }

    public class B
    {
        public virtual string FF()
        {
            return "B.FF";
        }
    }
}
