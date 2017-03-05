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
        /// <summary>
        /// page数据库对象
        /// </summary>
        /// <param name="opt"></param>
        public ExperimentPageContext(DbContextOptions<ExperimentPageContext> opt) : base(opt)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //确定UserRole表中的两个字段是联合主键
            modelBuilder.Entity<UserRole>().HasKey(u => new { u.UserID, u.RoleID });

            modelBuilder.Entity<RolePermission>().HasKey(u => new { u.PermissionID, u.RoleID });
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

        /// <summary>
        /// 权限
        /// </summary>
        public DbSet<Permission> Permissions
        { get; set; }
        /// <summary>
        /// 角色权限
        /// </summary>
        public DbSet<RolePermission> RolePermissions
        { get; set; }
    }
  


}