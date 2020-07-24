using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Asp.NetCore_WebPage.Model;

namespace Asp.NetCore_WebPage.Migrations
{
    [DbContext(typeof(ExperimentPageContext))]
    [Migration("20170306030107_ExperimentPageMigration")]
    partial class ExperimentPageMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Asp.NetCore_WebPage.Model.Permission", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActionName");

                    b.Property<string>("PermissionName");

                    b.HasKey("ID");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Asp.NetCore_WebPage.Model.Role", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("RoleName");

                    b.HasKey("ID");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Asp.NetCore_WebPage.Model.RolePermission", b =>
                {
                    b.Property<int>("PermissionID");

                    b.Property<int>("RoleID");

                    b.HasKey("PermissionID", "RoleID");

                    b.HasIndex("RoleID");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("Asp.NetCore_WebPage.Model.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.Property<string>("UserName");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Asp.NetCore_WebPage.Model.UserRole", b =>
                {
                    b.Property<int>("UserID");

                    b.Property<int>("RoleID");

                    b.HasKey("UserID", "RoleID");

                    b.HasIndex("RoleID");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Asp.NetCore_WebPage.Model.RolePermission", b =>
                {
                    b.HasOne("Asp.NetCore_WebPage.Model.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Asp.NetCore_WebPage.Model.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Asp.NetCore_WebPage.Model.UserRole", b =>
                {
                    b.HasOne("Asp.NetCore_WebPage.Model.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Asp.NetCore_WebPage.Model.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
