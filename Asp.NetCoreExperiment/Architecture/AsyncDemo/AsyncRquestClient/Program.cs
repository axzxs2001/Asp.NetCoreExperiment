using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AsyncRquestClient
{
    class ProgramAsync
    {
        static int times = 100;
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("输入循环次数");
                times = int.Parse(Console.ReadLine());

                #region 同步

                Console.WriteLine("-----------------同步调同步API------------------");
                SyncCallSyncAPI();
                Console.ReadLine();

                Console.WriteLine("-----------------同步调异步API------------------");
                SyncCallAsyncAPI();
                Console.ReadLine();

                Console.WriteLine("-----------------TaskFactory同步调同步API------------------");
                TaskFactorySyncCallSyncAPI();
                Console.ReadLine();

                Console.WriteLine("-----------------TaskFactory同步调异步API------------------");
                TaskFactorySyncCallAsyncAPI();
                Console.ReadLine();
                #endregion
             
                #region 异步
                Console.WriteLine("-----------------异步调异步API------------------");
                await AsyncCallAsyncAPI();
                Console.ReadLine();

                Console.WriteLine("-----------------异步调同步API------------------");
                await AsyncCallSyncAPI();
                Console.ReadLine();
            
                Console.WriteLine("-----------------TaskFactory异步调异步API------------------");
                await TaskFactoryAsyncCallAsyncAPI();
                Console.ReadLine();

                Console.WriteLine("-----------------TaskFactory异步调同步API------------------");
                await TaskFactoryAsyncCallSyncAPI();
                Console.ReadLine();
                #endregion
            }
        }
        #region 异常
        /// <summary>
        /// 异步调异步API
        /// </summary>
        /// <returns></returns>
        async static Task AsyncCallAsyncAPI()
        {
            var r = DeleteAllAsync().Result;
            Console.WriteLine($"异步调异步API开始时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")}");
            for (int i = 1; i <= times; i++)
            {
                try
                {
                    var student = new Student { Name = "张三" + i, Birthday = DateTime.Now, CardID = "C0000" + i, ClassID = 1, Sex = "男" };
                    using var client = new HttpClient();
                    client.BaseAddress = new Uri("https://localhost:5001");
                    var request = new HttpRequestMessage(HttpMethod.Post, "addstudentasync");
                    request.Content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var stu = JsonConvert.DeserializeObject<Student>(content);

                    }
                    else
                    {
                        var content = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine("同步调同步添加错误返回值：" + content);

                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }

        }
        /// <summary>
        /// TaskFactory异步调异步API
        /// </summary>
        /// <returns></returns>
        static async Task TaskFactoryAsyncCallAsyncAPI()
        {
            var r = await DeleteAllAsync();
            Console.WriteLine($"TaskFactory异步调异步开API始时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")}");
            for (int i = 1; i <= times; i++)
            {
                await Task.Factory.StartNew(async () =>
                {
                    try
                    {
                        var student = new Student { Name = "张三" + i, Birthday = DateTime.Now, CardID = "C0000" + i, ClassID = 1, Sex = "男" };
                        using var client = new HttpClient();
                        client.BaseAddress = new Uri("https://localhost:5001");
                        var request = new HttpRequestMessage(HttpMethod.Post, "addstudentasync");
                        request.Content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");
                        var response = await client.SendAsync(request);
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var stu = JsonConvert.DeserializeObject<Student>(content);

                        }
                        else
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            Console.WriteLine("异步调异步添加错误返回值：" + content);

                        }
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine(exc.Message);
                    }
                });
            }

        }

        /// <summary>
        /// 异步调同步API
        /// </summary>
        /// <returns></returns>
        async static Task AsyncCallSyncAPI()
        {
            var r = DeleteAllAsync().Result;
            Console.WriteLine($"异步调同步API开始时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")}");
            for (int i = 1; i <= times; i++)
            {
                try
                {
                    var student = new Student { Name = "张三" + i, Birthday = DateTime.Now, CardID = "C0000" + i, ClassID = 1, Sex = "男" };
                    using var client = new HttpClient();
                    client.BaseAddress = new Uri("https://localhost:5001");
                    var request = new HttpRequestMessage(HttpMethod.Post, "addstudent");
                    request.Content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");
                    var response = await client.SendAsync(request);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var stu = JsonConvert.DeserializeObject<Student>(content);

                    }
                    else
                    {
                        var content = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine("同步调同步添加错误返回值：" + content);

                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }

        }
        /// <summary>
        /// TaskFactory异步调同步API
        /// </summary>
        /// <returns></returns>
        static async Task TaskFactoryAsyncCallSyncAPI()
        {
            var r = await DeleteAllAsync();
            Console.WriteLine($"TaskFactory异步调同步API开始时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")}");
            for (int i = 1; i <= times; i++)
            {
                await Task.Factory.StartNew(async () =>
                {
                    try
                    {
                        var student = new Student { Name = "张三" + i, Birthday = DateTime.Now, CardID = "C0000" + i, ClassID = 1, Sex = "男" };
                        using var client = new HttpClient();
                        client.BaseAddress = new Uri("https://localhost:5001");
                        var request = new HttpRequestMessage(HttpMethod.Post, "addstudent");
                        request.Content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");
                        var response = await client.SendAsync(request);
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var stu = JsonConvert.DeserializeObject<Student>(content);

                        }
                        else
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            Console.WriteLine("异步调异步添加错误返回值：" + content);

                        }
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine(exc.Message);
                    }
                });
            }

        }
        #endregion

        #region 同步
        /// <summary>
        /// 同步调同步API
        /// </summary>
        static void SyncCallSyncAPI()
        {
            var r = DeleteAllAsync().Result;
            Console.WriteLine($"同步调同步开始时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")}");
            for (int i = 1; i <= times; i++)
            {
                try
                {
                    var student = new Student { Name = "张三" + i, Birthday = DateTime.Now, CardID = "C0000" + i, ClassID = 1, Sex = "男" };
                    using var client = new HttpClient();
                    client.BaseAddress = new Uri("https://localhost:5001");
                    var request = new HttpRequestMessage(HttpMethod.Post, "addstudent");
                    request.Content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");
                    var response = client.SendAsync(request).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var content = response.Content.ReadAsStringAsync().Result;
                        var stu = JsonConvert.DeserializeObject<Student>(content);

                    }
                    else
                    {
                        var content = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine("同步调同步添加错误返回值：" + content);

                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
        }
        static void TaskFactorySyncCallSyncAPI()
        {
            var r = DeleteAllAsync().Result;
            Console.WriteLine($"TaskFactory异步调同步API开始时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")}");
            for (int i = 1; i <= times; i++)
            {
                var result = Task.Factory.StartNew(async () =>
                  {
                      try
                      {
                          var student = new Student { Name = "张三" + i, Birthday = DateTime.Now, CardID = "C0000" + i, ClassID = 1, Sex = "男" };
                          using var client = new HttpClient();
                          client.BaseAddress = new Uri("https://localhost:5001");
                          var request = new HttpRequestMessage(HttpMethod.Post, "addstudent");
                          request.Content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");
                          var response = await client.SendAsync(request);
                          if (response.IsSuccessStatusCode)
                          {
                              var content = await response.Content.ReadAsStringAsync();
                              var stu = JsonConvert.DeserializeObject<Student>(content);

                          }
                          else
                          {
                              var content = await response.Content.ReadAsStringAsync();
                              Console.WriteLine("异步调异步添加错误返回值：" + content);

                          }
                      }
                      catch (Exception exc)
                      {
                          Console.WriteLine(exc.Message);
                      }
                  }).Result;
            }

        }
        /// <summary>
        /// 同步调异步API
        /// </summary>
        static void SyncCallAsyncAPI()
        {
            var r = DeleteAllAsync().Result;
            Console.WriteLine($"同步调异步开始时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")}");

            for (int i = 1; i <= times; i++)
            {
                try
                {
                    var student = new Student { Name = "张三" + i, Birthday = DateTime.Now, CardID = "C0000" + i, ClassID = 1, Sex = "男" };
                    using var client = new HttpClient();
                    client.BaseAddress = new Uri("https://localhost:5001");
                    var request = new HttpRequestMessage(HttpMethod.Post, "addstudentasync");
                    request.Content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");
                    var response = client.SendAsync(request).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var content = response.Content.ReadAsStringAsync().Result;
                        var stu = JsonConvert.DeserializeObject<Student>(content);

                    }
                    else
                    {
                        var content = response.Content.ReadAsStringAsync().Result;
                        Console.WriteLine("同步调异步添加错误返回值：" + content);

                    }
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
        }

        static void TaskFactorySyncCallAsyncAPI()
        {
            var r = DeleteAllAsync().Result;
            Console.WriteLine($"TaskFactory异步调同步API开始时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")}");
            for (int i = 1; i <= times; i++)
            {
                var result = Task.Factory.StartNew(async () =>
                {
                    try
                    {
                        var student = new Student { Name = "张三" + i, Birthday = DateTime.Now, CardID = "C0000" + i, ClassID = 1, Sex = "男" };
                        using var client = new HttpClient();
                        client.BaseAddress = new Uri("https://localhost:5001");
                        var request = new HttpRequestMessage(HttpMethod.Post, "addstudentasync");
                        request.Content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");
                        var response = await client.SendAsync(request);
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            var stu = JsonConvert.DeserializeObject<Student>(content);

                        }
                        else
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            Console.WriteLine("异步调异步添加错误返回值：" + content);

                        }
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine(exc.Message);
                    }
                }).Result;
            }

        }
        #endregion



        static async Task<bool> DeleteAllAsync()
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5001");
            var request = new HttpRequestMessage(HttpMethod.Delete, "deleteall");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<bool>(content);
                return result;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("删除错误返回值：" + content);
                return false;
            }
        }
    }

    public class Student
    {
        public string StuNo { get; set; }
        public string Name { get; set; }
        public string CardID { get; set; }
        public string Sex { get; set; }
        public DateTime Birthday { get; set; }
        public int ClassID { get; set; }

    }



}
