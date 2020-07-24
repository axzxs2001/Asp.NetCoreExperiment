using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace RefitDemo1
{
    class Program
    {
        static void Main(string[] args)
        {
            var userManage = RestService.For<IUserManage>("http://localhost:5000");
            while (true)
            {
                try
                {
                    Console.WriteLine("Refit Demo");

                    Console.WriteLine("1、Admin登录  2、System登录 3、查询用户 4、动态返回值查询用户  5、添加用户");
                    var check = Console.ReadLine();
                    switch (check)
                    {
                        case "1":
                            AdminLogin(ref userManage);
                            break;
                        case "2":
                            SystemLogin(ref userManage);
                            break;
                        case "3":
                            QueryAllUser(userManage);
                            break;
                        case "4":
                            QueryAllUser1(userManage);
                            break;
                        case "5":
                            AddUser(userManage);
                            break;
                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
        }

        static void AdminLogin(ref IUserManage userManage)
        {
            //登录获取Token
            Console.WriteLine("用户名：gsw");
            var userName = "gsw";
            Console.WriteLine("密码：111111");
            var password = "111111";

            var result = userManage.Login(userName, password).GetAwaiter().GetResult();
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            //初始化通用接口
            userManage = RestService.For<IUserManage>(new HttpClient(new AuthenticatedHttpClientHandler(result.access_token)) { BaseAddress = new Uri("http://localhost:5000") });
        }
        static void SystemLogin(ref IUserManage userManage)
        {
            //登录获取Token
            Console.WriteLine("用户名：ggg");
            var userName = "ggg";
            Console.WriteLine("密码：222222");
            var password = "222222";

            var result = userManage.Login(userName, password).GetAwaiter().GetResult();
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
            //初始化通用接口
            userManage = RestService.For<IUserManage>(new HttpClient(new AuthenticatedHttpClientHandler(result.access_token)) { BaseAddress = new Uri("http://localhost:5000") });
        }

        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="userManage"></param>
        static void QueryAllUser(IUserManage userManage)
        {
            foreach (var user in userManage.GetUsers().Result)
            {
                Console.WriteLine(user);
            }
        }
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="userManage"></param>
        static void QueryAllUser1(IUserManage userManage)
        {
            foreach (var user in userManage.GetUsers1().Result)
            {
                Console.WriteLine(user);
            }
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="userManage"></param>
        static void AddUser(IUserManage userManage)
        {
            var user = new User { ID = DateTime.Now.Millisecond, Name = "李四", Age = 123 };
            var result = userManage.AddUser(user).Result;
            Console.WriteLine(result);           
        }
    }
    [Headers("Authorization: Bearer")]
    public interface IUserManage
    {

        [Post("/authapi/login")]
        Task<Result> Login(string username, string password);
      
        [Get("/users")]
        Task<List<User>> GetUsers();

     
        [Get("/users")]
        Task<List<dynamic>> GetUsers1();

    
        [Post("/adduser")]
        Task<bool> AddUser([Body]User user);

    }
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }
    }

    public class Result
    {
        public bool status { get; set; }
        public string access_token { get; set; }

        public string expires_in { get; set; }
        public string token_type { get; set; }
    }

    public class AuthenticatedHttpClientHandler : HttpClientHandler
    {

        string _token;
        public AuthenticatedHttpClientHandler(string token)
        {
            _token = token;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // See if the request has an authorize header
            var auth = request.Headers.Authorization;
            if (auth != null)
            {
                // var token = await getToken().ConfigureAwait(false);
                request.Headers.Authorization = new AuthenticationHeaderValue(auth.Scheme, _token);
            }
            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }

}
