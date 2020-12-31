using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AsyncRquestClient
{
    class Program
    {
        static int times = 100;
        static async Task Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("输入循环次数");
                times = int.Parse(Console.ReadLine());
                Console.WriteLine("-----------------------------------");
                Call();
                Console.ReadLine();
                await CallAsync();
            }
        }
        static void Call()
        {
            var r = DeleteAllAsync().Result;
            Console.WriteLine($"同步开始时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")}");

            for (int i = 1; i <= times; i++)
            {
                try
                {
                    var stu = new Student { Name = "张三" + i, Birthday = DateTime.Now, CardID = "C0000" + i, ClassID = 1, Sex = "男" };
                    var result = AddStudent(stu);
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
        }

        static Student AddStudent(Student student)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5001");
            var request = new HttpRequestMessage(HttpMethod.Post, "addstudent");
            request.Content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");
            var response = client.SendAsync(request).Result;
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var stu = JsonConvert.DeserializeObject<Student>(content);
                return stu;
            }
            else
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("同步添加错误返回值：" + content);
                return null;
            }
        }

        static async Task CallAsync()
        {
            var r = await DeleteAllAsync();
            Console.WriteLine($"导步开始时间：{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.ffff")}");
            for (int i = 1; i <= times; i++)
            {
                await Task.Factory.StartNew(async () =>
                {
                    try
                    {
                        var stu = new Student { Name = "张三" + i, Birthday = DateTime.Now, CardID = "C0000" + i, ClassID = 1, Sex = "男" };
                        var result = await AddStudentAsync(stu);
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine(exc.Message);
                    }
                });
            }
        }
        static async Task<Student> AddStudentAsync(Student student)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:5001");
            var request = new HttpRequestMessage(HttpMethod.Post, "addstudentasync");
            request.Content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var stu = JsonConvert.DeserializeObject<Student>(content);
                return stu;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine("异步添加错误返回值：" + content);
                return null;
            }
        }





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
