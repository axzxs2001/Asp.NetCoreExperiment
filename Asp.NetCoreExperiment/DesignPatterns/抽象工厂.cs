using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    /****************************************************************************
    * 抽象工厂
    * 提供一个创建一系列相关或相互依赖的接口，而无需指定它们具体的类。
    ****************************************************************************/

    /// <summary>
    /// 部门实体类
    /// </summary>
    public class Department
    {
    }
    /// <summary>
    /// 部门操作类
    /// </summary>
    public interface IDepartment
    {
        void Insert(Department department);
        Department GetDepartment(int id);
    }
    /// <summary>
    /// sql部门操作类
    /// </summary>
    public class SqlserverDepartment : IDepartment
    {
        public Department GetDepartment(int id)
        {
            Console.WriteLine("SqlserverDepartment.GetDeparment");
            return null;
        }

        public void Insert(Department department)
        {
            Console.WriteLine("SqlserverDepartment.Insert");
        }
    }
    /// <summary>
    /// oracle部门操作类
    /// </summary>
    public class OracleDepartment : IDepartment
    {
        public Department GetDepartment(int id)
        {
            Console.WriteLine("OracleDepartment.GetDeparment");
            return null;
        }

        public void Insert(Department department)
        {
            Console.WriteLine("OracleDepartment.Insert");
        }
    }
    /// <summary>
    /// 数据库操作创建工厂
    /// </summary>
    public interface IDataBaseFactory
    {
        IDepartment CreateDepartment();
    }
    /// <summary>
    /// sql数据库操作创建工厂
    /// </summary>
    public class SqlserverFactory : IDataBaseFactory
    {
        public IDepartment CreateDepartment()
        {
            return new SqlserverDepartment();
        }
    }
    /// <summary>
    /// oracle数据库操作创建工厂
    /// </summary>
    public class OracleFactory : IDataBaseFactory
    {
        public IDepartment CreateDepartment()
        {
            return new OracleDepartment();
        }
    }
}
