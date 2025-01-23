using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using WebApi.Domain.Entities;

namespace WebApi.Repositories.WebApiDB
{
    public class WebApiDbContext : DbContext
    {
        public WebApiDbContext()
        {
        }

        public WebApiDbContext(DbContextOptions<WebApiDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<T_Button> T_Buttons { get; set; } = null!;
        public virtual DbSet<T_ButtonPermission> T_ButtonPermissions { get; set; } = null!;
        public virtual DbSet<T_Email> T_Emails { get; set; } = null!;
        public virtual DbSet<T_Log> T_Logs { get; set; } = null!;
        public virtual DbSet<T_Menu> T_Menus { get; set; } = null!;
        public virtual DbSet<T_MenuButton> T_MenuButtons { get; set; } = null!;
        public virtual DbSet<T_Org> T_Orgs { get; set; } = null!;
        public virtual DbSet<T_Resource> T_Resources { get; set; } = null!;
        public virtual DbSet<T_Role> T_Roles { get; set; } = null!;
        public virtual DbSet<T_User> T_Users { get; set; } = null!;
        public virtual DbSet<T_UserOrg> T_UserOrgs { get; set; } = null!;
        public virtual DbSet<T_UserRole> T_UserRoles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(); // 启用敏感数据记录
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=WebApi;Integrated Security=False;User ID=sa;Password=123456;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<T_Button>(entity =>
            {
                entity.ToTable("T_Button");

                entity.Property(e => e.Code).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Action)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Icon)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifyTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.T_Buttons)
                    .HasForeignKey(d => d.MenuCode)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__T_Button__Modify__3CF40B7E");
            });

            modelBuilder.Entity<T_ButtonPermission>(entity =>
            {
                entity.Property(e => e.Code).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifyTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Type)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.HasOne(d => d.Button)
                    .WithMany(p => p.T_ButtonPermissions)
                    .HasForeignKey(d => d.ButtonCode)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__T_ButtonP__Modif__42ACE4D4");
            });

            modelBuilder.Entity<T_Email>(entity =>
            {
                entity.ToTable("T_Email");

                entity.Property(e => e.Code).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Body)
                    .HasMaxLength(8000)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Recipient)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Subject)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.SendUser)
                    .WithMany(p => p.T_Emails)
                    .HasForeignKey(d => d.SendUserCode)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__T_Email__CreateU__589C25F3");
            });

            modelBuilder.Entity<T_Log>(entity =>
            {
                entity.ToTable("T_Log");

                entity.Property(e => e.Code).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Action)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.IP)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.T_Logs)
                    .HasForeignKey(d => d.UserCode)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__T_Log__CreateUse__53D770D6");
            });

            modelBuilder.Entity<T_Menu>(entity =>
            {
                entity.ToTable("T_Menu");

                entity.Property(e => e.Code).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Icon)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifyTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.URL)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<T_MenuButton>(entity =>
            {
                entity.Property(e => e.Code).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifyTime).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Button)
                    .WithMany(p => p.T_MenuButtons)
                    .HasForeignKey(d => d.ButtonCode)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__T_MenuBut__Modif__4F12BBB9");
            });

            modelBuilder.Entity<T_Resource>(entity =>
            {
                entity.Property(e => e.Code).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifyTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Path)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<T_Org>(entity =>
            {
                entity.ToTable("T_Org");

                entity.Property(e => e.Code).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifyTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NodeType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<T_Role>(entity =>
            {
                entity.ToTable("T_Role");

                entity.Property(e => e.Code).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifyTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<T_User>(entity =>
            {
                entity.ToTable("T_User");

                entity.Property(e => e.Code).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FullName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModifyTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });
            modelBuilder.Entity<T_UserOrg>(entity =>
            {
                entity.Property(e => e.Code).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifyTime).HasDefaultValueSql("(getdate())");


                modelBuilder.Entity<T_UserOrg>()
                    .HasOne(ur => ur.User)
                    .WithMany(u => u.T_UserOrgs)
                    .HasForeignKey(ur => ur.UserCode);

                modelBuilder.Entity<T_UserOrg>()
                    .HasOne(ur => ur.Org)
                    .WithMany(r => r.T_UserOrgs)
                    .HasForeignKey(ur => ur.OrgCode);
            });
            modelBuilder.Entity<T_UserRole>(entity =>
            {
                entity.Property(e => e.Code).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreateTime).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifyTime).HasDefaultValueSql("(getdate())");


                modelBuilder.Entity<T_UserRole>()
                    .HasOne(ur => ur.User)
                    .WithMany(u => u.T_UserRoles)
                    .HasForeignKey(ur => ur.UserCode);

                modelBuilder.Entity<T_UserRole>()
                    .HasOne(ur => ur.Role)
                    .WithMany(r => r.T_UserRoles)
                    .HasForeignKey(ur => ur.RoleCode);

            });
        }
    }
}
