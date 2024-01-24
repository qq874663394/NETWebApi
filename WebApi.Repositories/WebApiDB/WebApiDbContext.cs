using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using WebApi.Domain.Entities.WebApiDB;

namespace WebApi.Repositories.WebApiDB
{
    public partial class WebApiDbContext : DbContext
    {
        public WebApiDbContext()
        {
        }

        public virtual DbSet<T_Module> Modules { get; set; } = null!;
        public virtual DbSet<T_ModuleElement> ModuleElements { get; set; } = null!;
        public virtual DbSet<T_Relevance> Relevances { get; set; } = null!;
        public virtual DbSet<T_Role> Roles { get; set; } = null!;
        public virtual DbSet<T_User> Users { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(true);  //允许打印参数
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<T_Module>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("T_Module");

                entity.Property(e => e.CascadeId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HotKey)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.IconName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ParentId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ParentName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Url)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Vector)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<T_ModuleElement>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("T_ModuleElement");

                entity.Property(e => e.Attr)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Class)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.DomId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Icon)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModuleId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Remark)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.Script)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TypeName).HasMaxLength(20);
            });

            modelBuilder.Entity<T_Relevance>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("T_Relevance");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.ExtendInfo)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ModelName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.OperateTime)
                .HasColumnType("datetime");

                entity.Property(e => e.OperatorId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SecondId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ThirdId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<T_Role>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("T_Role");

                entity.Property(e => e.CreateId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TypeName).HasMaxLength(20);
            });

            modelBuilder.Entity<T_User>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("T_User");

                entity.Property(e => e.Account)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.BizCode)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreateId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.TypeId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TypeName).HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
