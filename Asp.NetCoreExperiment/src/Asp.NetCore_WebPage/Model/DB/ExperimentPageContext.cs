using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Asp.NetCore_WebPage.Model
{
    /// <summary>
    /// page数据库对象
    /// </summary>
    public class ExperimentPageContext : DbContext
    {
        public ExperimentPageContext(DbContextOptions<ExperimentPageContext> opt) : base(opt)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //确定UserRole表中的两个字段是联合主键
            modelBuilder.Entity<UserRole>().HasKey(u => new { u.UserID, u.RoleID });
        }
        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<User> Users
        { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public DbSet<Role> Roles
        { get; set; }
        /// <summary>
        /// 用户角色
        /// </summary>
        public DbSet<UserRole> UserRoles
        { get; set; }
    }
    /// <summary>
    /// 用户表
    /// </summary>
    public class User
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]        
        public int ID
        { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        { get; set; }
        /// <summary>
        /// 用户角色集合
        /// </summary>
        public List<UserRole> UserRoles { get; set; }
    }
    /// <summary>
    /// 角色表
    /// </summary>
    public class Role
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int ID
        { get; set; }
        /// <summary>
        /// 角色名
        /// </summary>
        public string RoleName
        {
            get; set;
        }
        /// <summary>
        /// 用户角色列表
        /// </summary>
        public List<UserRole> UserRoles { get; set; }
    }
    /// <summary>
    /// 用户角色关系
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        { get; set; }
        /// <summary>
        /// 角色ID
        /// </summary>
        public int RoleID
        { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public Role Role { get; set; }
    }
}