using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Data.Entity
{
    public partial class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Log> Logs { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<PermissionGroup> PermissionGroups { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("User ID=dbuser;Password=y2VtyFGgM3U9;Server=db.phoesoftware.com;Port=54320;Database=empty_db;Integrated Security=true;Pooling=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("log");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Data).HasColumnName("data");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("permission");

                entity.HasIndex(e => e.Name, "permission_name_uindex")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreatedTime)
                    .HasColumnName("created_time")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Dependencies)
                    .HasColumnName("dependencies")
                    .HasDefaultValueSql("'{}'::integer[]");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.ParentPermissionId).HasColumnName("parent_permission_id");

                entity.Property(e => e.PermissionGroup).HasColumnName("permission_group");

                entity.HasOne(d => d.ParentPermission)
                    .WithMany(p => p.InverseParentPermission)
                    .HasForeignKey(d => d.ParentPermissionId)
                    .HasConstraintName("permission_permission_id_fk");

                entity.HasOne(d => d.PermissionGroupNavigation)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.PermissionGroup)
                    .HasConstraintName("permission_permission_group_id_fk");
            });

            modelBuilder.Entity<PermissionGroup>(entity =>
            {
                entity.ToTable("permission_group");

                entity.HasIndex(e => e.Id, "permission_group_id_uindex")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.PermissionIdList)
                    .HasColumnName("permission_id_list")
                    .HasDefaultValueSql("'{}'::integer[]");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Email, "user_email_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Id, "user_id_uindex")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "user_username_uindex")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(500)
                    .HasColumnName("email");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(255)
                    .HasColumnName("lastname");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .HasColumnName("name");

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(1024)
                    .HasColumnName("password_hash");

                entity.Property(e => e.PasswordSalt)
                    .HasMaxLength(256)
                    .HasColumnName("password_salt");

                entity.Property(e => e.PermissionIdList)
                    .HasColumnName("permission_id_list")
                    .HasDefaultValueSql("'{}'::integer[]");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .HasColumnName("phone")
                    .HasDefaultValueSql("''::character varying");

                entity.Property(e => e.Username)
                    .HasMaxLength(500)
                    .HasColumnName("username");

                entity.Property(e => e.VisibleName)
                    .HasMaxLength(255)
                    .HasColumnName("visible_name")
                    .HasDefaultValueSql("''::character varying");

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserRole",
                        l => l.HasOne<Role>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("user_role_role_id_fk"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("user_role_user_id_fk"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId").HasName("user_role_pk");

                            j.ToTable("user_role");

                            j.IndexerProperty<Guid>("UserId").HasColumnName("user_id");

                            j.IndexerProperty<long>("RoleId").HasColumnName("role_id");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
