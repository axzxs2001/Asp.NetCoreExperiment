using HisMedical;
using System;
using System.Linq;
using System.Reflection;

namespace ArchitectureDemo05
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {

                Console.WriteLine("1、东软  2、银海");
                var no = Console.ReadLine();
                Console.WriteLine("1、住院登记   2、住院结算");
                var busNo = Console.ReadLine();
                var path = "";
                switch (no)
                {
                    case "1":
                        path = @"C:\MyFile\Source\Repos\Asp.NetCoreExperiment\Asp.NetCoreExperiment\Architecture\NeusoftMedical\bin\Debug\netstandard2.0\NeusoftMedical.dll";
                        break;
                    case "2":
                        path = @"C:\MyFile\Source\Repos\Asp.NetCoreExperiment\Asp.NetCoreExperiment\Architecture\YiHaiMedical\bin\Debug\netstandard2.0\YiHaiMedical.dll";
                        break;
                }
                //从文件加载应用程序集并得到具体类型
                var medicalType = Assembly.LoadFile(path).GetTypes().FirstOrDefault(t => t.GetInterfaces().Where(s => s.Name == "IHis").Count() > 0);
                IHis his = (IHis)Activator.CreateInstance(medicalType);
                var registerID = "";
                switch (busNo)
                {
                    case "1":
                        registerID = Register(his);
                        break;
                    case "2":
                        if (registerID != "")
                        {

                            Fee(his, registerID);
                        }
                        else
                        {
                            Console.WriteLine("请先登记住院");
                        }
                        break;
                }
            }
        }
        /// <summary>
        /// his登记住院
        /// </summary>
        /// <param name="his"></param>
        /// <returns></returns>
        static string Register(IHis his)
        {
            var registerID = DateTime.Now.ToString("yyyyMMddHHmmss");
            Console.WriteLine($"*****完成His的登记，登记号:{registerID}");
            his.RegisterID = registerID;
            var result = his.Register();
            return registerID;
        }
        /// <summary>
        /// his缴费
        /// </summary>
        /// <param name="his"></param>
        /// <param name="registerID"></param>
        /// <returns></returns>
        static bool Fee(IHis his, string registerID)
        {
            Console.WriteLine($"*****完成His的结算，登记号:{registerID}");
            his.RegisterID = registerID;
            var result = his.Fee();
            return true;
        }
    }







}
