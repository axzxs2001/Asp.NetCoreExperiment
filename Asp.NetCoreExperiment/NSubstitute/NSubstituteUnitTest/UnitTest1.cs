using Microsoft.VisualBasic;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace NSubstituteUnitTest
{
    public class UserManageUnitTest
    {
        IUserService _userService;
        UserManage _userManage;
        public UserManageUnitTest()
        {
            _userService = Substitute.For<IUserService>();
            _userManage = new UserManage(_userService);
        }
        [Fact]
        public async Task AddUser_Default_Return()
        {
            var user = new User { UserName = "a", Password = "b" };
            _userService.AddUserAsync(Arg.Any<User>()).Returns(true);
            var result = await _userManage.AddUserAsync(user);
            Assert.Equal(true, result);
        }

        [Fact]
        public async Task AddUser_ServiceError_ReturnFalse()
        {
            var user = new User { UserName = "a", Password = "b" };
            _userService.AddUserAsync(Arg.Any<User>()).Returns(false);
            var result = await _userManage.AddUserAsync(user);
            Assert.Equal(false, result);
        }
        [Fact]
        public async Task AddUser_Exception_ReturnFalse()
        {
            var user = new User { UserName = "a", Password = "b" };
            _userService.AddUserAsync(Arg.Any<User>()).Throws(new Exception("异常"));
            var result = await _userManage.AddUserAsync(user);
            Assert.Equal(false, result);
        }
        [Theory]
        [InlineData("", "a")]
        [InlineData("a", "")]
        [InlineData("", "")]
        [InlineData(null, "a")]
        [InlineData("a", null)]
        [InlineData(null, null)]
        [InlineData(" ", "a")]
        [InlineData("a", " ")]
        [InlineData(" ", " ")]
        public async Task AddUser_IsNullOrEmpty_ReturnFalse(string userName, string password)
        {
            var user = new User { UserName = userName, Password = password };
            var result = await _userManage.AddUserAsync(user);
            Assert.Equal(false, result);
        }
        
        [Theory]
        [InlineData("稀饭", "烂代码")]
        [InlineData("饺子", "好代码")]
        [InlineData("饸饹", "优代码")]
        public void Code_DifferentFood_DifferentResults(string food, string result)
        {
            //Arrange
            var guiSuWei = new Programmer();
            //Act
            var codeResult = guiSuWei.Code(food);
            //Assert
            Assert.Equal(result, codeResult);
        }
    }
    public class Programmer
    {
        public string Code(string food)
        {
            return "";
        }
    }

    public class UserManage
    {
        private readonly IUserService _userService;
        public UserManage(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<bool> AddUserAsync(User user)
        {
            try
            {
                if (!string.IsNullOrEmpty(user.UserName.Trim()) && !string.IsNullOrEmpty(user.Password.Trim()))
                {
                    return await _userService.AddUserAsync(user);
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
    }
    public interface IUserService
    {
        Task<bool> AddUserAsync(User user);
    }

    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
