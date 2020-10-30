using System;

namespace ArchitectureDemo04
{
    class Program
    {
        static void Main(string[] args)
        {
            var backQueryCard = Send(new QueryCardEntity { PersonNumber = "0000001", ICCardNumber = "C00000001" });

            var backDoctorQuery = Send(new DoctorQuery { DoctorCode = "0001" });
        }
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        static Entity Send(Entity entity)
        {
            try
            {
                foreach (var att in entity.GetType().GetCustomAttributes(false))
                {
                    if (att is PackageTypeAttribute)
                    {
                        var attPackage = att as PackageTypeAttribute;    
                        Console.WriteLine($"入参：");
                        Console.WriteLine(entity);
                        Console.WriteLine("模拟函数调用：");
                        Console.WriteLine($"OltpTransData({attPackage.OperationType},{attPackage.DataFormaterType},{attPackage.MinLength},{entity})");
                        var backContent = BackOperation(entity);
                        var backEntity = entity.ToEntity(entity.GetType(),backContent);
                        return backEntity;
                    }
                }
                return null;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 模拟医保中心返回
        /// </summary>
        /// <param name="entity">参数</param>
        /// <returns></returns>
        static string BackOperation(Entity entity)
        {
            switch (entity.GetType().Name)
            {
                case "QueryCardEntity":
                    return " 0000001                Jack210213198411113111C00000001   1A   1000.66         0         0         0      1800A00131   0   0"; 
                case "DoctorQuery":
                    return "    0001            DcotorLi210211198707182233            0002011320201029190850  1";
            }
            return null;
        }
    }
}
