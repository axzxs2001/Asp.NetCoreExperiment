//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace GPT_Demo02_Ext
//{
//    public class TenantService
//    {
//    }
//    //请给出一个系统的租户应该有那些属性，不用作详细说明

//    //把下面的属性加上类型
//    //租户属性：
//    //1. 租户ID
//    //2. 租户名称
//    //3. 租户类型
//    //4. 租户状态
//    //5. 租户创建时间
//    //6. 租户有效期
//    //7. 租户联系人
//    //8. 租户联系方式
//    //9. 租户地址
//    //10. 租户备注

//    //请把下面的属性生成一张数据库表，mysql数据库
//    //1. 租户ID: number
//    //2. 租户名称: string
//    //3. 租户类型: string
//    //4. 租户状态: bool
//    //5. 租户创建时间: Date
//    //6. 租户有效期: Date
//    //7. 租户联系人: string
//    ////8. 租户联系方式: string
//    //9. 租户地址: string
//    //10. 租户备注: string

//    //用dapper实现对Tenant实体类的增删除查

//    public class TenantDapper
//    {
//        private readonly IDbConnection _connection;
//        public TenantDapper(IDbConnection connection)
//        {
//            _connection = connection;
//        }

//        //查询
//        public IEnumerable<Tenant> GetAll()
//        {
//            return _connection.Query<Tenant>("SELECT * FROM Tenant");
//        }      

//        //添加
//        public void Add(Tenant tenant)
//        {
//            _connection.Execute("INSERT INTO Tenant (Name, HostName, IsActive) VALUES (@Name, @HostName, @IsActive)", tenant);
//        }

//        //更新
//        public void Update(Tenant tenant)
//        {
//            _connection.Execute("UPDATE Tenant SET Name = @Name, HostName = @HostName, IsActive = @IsActive WHERE Id = @Id", tenant);
//        }

//        //删除
//        public void Delete(int id)
//        {
//            _connection.Execute("DELETE FROM Tenant WHERE Id = @Id", new { Id = id });
//        }
//    }
//}

//public class Tenant
//{
//    /// <summary>
//    /// 租户ID
//    /// </summary>
//    public int Number { get; set; }

//    /// <summary>
//    /// 租户名称
//    /// </summary>
//    public string Name { get; set; }

//    /// <summary>
//    /// 租户类型
//    /// </summary>
//    public string Type { get; set; }

//    /// <summary>
//    /// 租户状态
//    /// </summary>
//    public bool Status { get; set; }

//    /// <summary>
//    /// 租户创建时间
//    /// </summary>
//    public DateTime CreateTime { get; set; }

//    /// <summary>
//    /// 租户有效期
//    /// </summary>
//    public DateTime Validity { get; set; }

//    /// <summary>
//    /// 租户联系人
//    /// </summary>
//    public string Contact { get; set; }

//    /// <summary>
//    /// 租户联系方式
//    /// </summary>
//    public string ContactWay { get; set; }

//    /// <summary>
//    /// 租户地址
//    /// </summary>
//    public string Address { get; set; }

//    /// <summary>
//    /// 租户备注
//    /// </summary>
//    public string Remark { get; set; }
//}

