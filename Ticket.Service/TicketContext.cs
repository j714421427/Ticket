using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Ticket.Model;
using Ticket.Model.Enums;

namespace Ticket.Service
{
    public class TicketContext : DbContext
    {
        public TicketContext()
        {

        }

        public TicketContext(DbContextOptions<TicketContext> options)
            : base(options)
        {
            if (options == null)
            {
                options = DbOptionsFactory.DbContextOptions;
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<PermissionGroup> PermissionGroups { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<TicketTack> TicketTasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User
            modelBuilder.Entity<User>().Property(p => p.Name)
                .HasMaxLength(20);
            modelBuilder.Entity<User>().Property(p => p.Account)
                .HasMaxLength(20);
            modelBuilder.Entity<User>().Property(p => p.Password)
                .HasMaxLength(50);

            // Role
            modelBuilder.Entity<Role>().Property(p => p.Name)
                .HasMaxLength(20);

            // PermissionGroup
            modelBuilder.Entity<PermissionGroup>().Property(p => p.Name)
                .HasMaxLength(20);

            // Permission
            modelBuilder.Entity<Permission>().Property(p => p.Name)
                .HasMaxLength(20);

            // FK

            // Permission
            modelBuilder.Entity<PermissionGroup>().HasMany(m => m.Permissions)
                .WithOne(o => o.PermissionGroup)
                .HasForeignKey(k => k.PermissionGroupId);

            // UserRole
            modelBuilder.Entity<User>().HasOne(r => r.Role)
             .WithMany(o => o.Users)
             .HasForeignKey(k => k.RoleId)
             .IsRequired();

            // RolePersmission
            modelBuilder.Entity<RolePermission>().HasKey(k => new { k.RoleId, k.PermissionId });

            modelBuilder.Entity<RolePermission>().HasOne(o => o.Role)
                .WithMany(m => m.RolePermissions)
                .HasForeignKey(k => k.RoleId);

            modelBuilder.Entity<RolePermission>().HasOne(o => o.Permission)
                .WithMany(m => m.RolePermissions)
                .HasForeignKey(k => k.PermissionId);


            // Task
            modelBuilder.Entity<TicketTack>().Property(p => p.Title)
                .HasMaxLength(20);
            modelBuilder.Entity<TicketTack>().Property(p => p.Content)
                .HasMaxLength(500);

            #region SEED

            var admin = new User { Id = 1, Name = "Admin", Account = "Admin", Password = "123qwe", Status = EntityStatus.Enabled };
            var sunny = new User { Id = 2, Name = "Sunny", Account = "Sunny001", Password = "123qwe", Status = EntityStatus.Enabled };
            var timmy = new User { Id = 3, Name = "Timmy", Account = "Timmy001", Password = "123qwe", Status = EntityStatus.Enabled };


            var sa = new Role { Id = 1, Name = "SA", CreatedAt = DateTime.Now, CreatedById = admin.Id };
            var rd = new Role { Id = 2, Name = "RD", CreatedAt = DateTime.Now, CreatedById = admin.Id };
            var qa = new Role { Id = 3, Name = "QA", CreatedAt = DateTime.Now, CreatedById = admin.Id };

            // Role
            modelBuilder.Entity<Role>().HasData(sa);
            modelBuilder.Entity<Role>().HasData(rd);
            modelBuilder.Entity<Role>().HasData(qa);

            // User
            sunny.RoleId = rd.Id;
            timmy.RoleId = qa.Id;
            admin.RoleId = sa.Id;
            modelBuilder.Entity<User>().HasData(admin);
            modelBuilder.Entity<User>().HasData(sunny);
            modelBuilder.Entity<User>().HasData(timmy);

            // Permission RolePermission
            var permissionGroups = new List<PermissionGroup>();

            int index = 1;

            foreach (PermissionGroupCode item in Enum.GetValues(typeof(PermissionGroupCode)))
            {
                permissionGroups.Add(new PermissionGroup() { Id = index, Name = item.ToString(), GroupCode = item });
                index++;
            }

            modelBuilder.Entity<PermissionGroup>().HasData(permissionGroups);

            index = 1;
            var permission = new Permission();
            var permissions = new List<Permission>();
            var saRolePermissions = new List<RolePermission>();
            var qaRolePermissions = new List<RolePermission>();
            var rdRolePermissions = new List<RolePermission>();

            foreach (PermissionCode item in Enum.GetValues(typeof(PermissionCode)))
            {
                var groupId = 1;

                switch (item)
                {
                    case PermissionCode.TicketTask_Access:
                    case PermissionCode.TicketTask_Edit:
                    case PermissionCode.TicketTask_Resolve:
                    case PermissionCode.TicketTask_Delete:
                        groupId = permissionGroups.Where(s => s.GroupCode == PermissionGroupCode.TicketTask).Select(s => s.Id).FirstOrDefault();
                        break;
                    case PermissionCode.Role_Access:
                    case PermissionCode.Role_Edit:
                    case PermissionCode.Role_Delete:
                    case PermissionCode.User_Access:
                    case PermissionCode.User_Edit:
                    case PermissionCode.User_Delete:
                    case PermissionCode.Setting_Access:
                    case PermissionCode.Setting_Edit:
                    case PermissionCode.Setting_Delete:
                        groupId = permissionGroups.Where(s => s.GroupCode == PermissionGroupCode.Setting).Select(s => s.Id).FirstOrDefault();
                        break;
                    default:
                        break;
                }

                permissions.Add(new Permission() { Id = index, Name = item.ToString(), PermissionCode = item, PermissionGroupId = groupId });
                saRolePermissions.Add(new RolePermission() { RoleId = sa.Id, PermissionId = index });
                index++;
            }
            permission = permissions.Where(q => q.PermissionCode == PermissionCode.TicketTask_Access).FirstOrDefault();
            rdRolePermissions.Add(new RolePermission() { RoleId = rd.Id, PermissionId = permission.Id });
            qaRolePermissions.Add(new RolePermission() { RoleId = qa.Id, PermissionId = permission.Id });
            permission = permissions.Where(q => q.PermissionCode == PermissionCode.TicketTask_Resolve).FirstOrDefault();
            rdRolePermissions.Add(new RolePermission() { RoleId = rd.Id, PermissionId = permission.Id });
            permission = permissions.Where(q => q.PermissionCode == PermissionCode.TicketTask_Edit).FirstOrDefault();
            qaRolePermissions.Add(new RolePermission() { RoleId = qa.Id, PermissionId = permission.Id });
            permission = permissions.Where(q => q.PermissionCode == PermissionCode.TicketTask_Delete).FirstOrDefault();
            qaRolePermissions.Add(new RolePermission() { RoleId = qa.Id, PermissionId = permission.Id });

            modelBuilder.Entity<Permission>().HasData(permissions);
            modelBuilder.Entity<RolePermission>().HasData(saRolePermissions);
            modelBuilder.Entity<RolePermission>().HasData(rdRolePermissions);
            modelBuilder.Entity<RolePermission>().HasData(qaRolePermissions);

            #endregion


            base.OnModelCreating(modelBuilder);
        }



        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    var connectionString = DbOptionsFactory.ConnectionString;
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder
        //            .UseSqlServer(connectionString);
        //    }
        //}
    }
}
