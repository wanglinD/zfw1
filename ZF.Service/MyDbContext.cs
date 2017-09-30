using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZF.Service.Entities
{
   public class MyDbContext:DbContext
    {
        public MyDbContext():base("name=connstr")
        {
            //禁用自动创建表
            Database.SetInitializer<MyDbContext>(null);

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //从所在的陈旭集中加在所有继承自EntityTypConfiguration
            modelBuilder.Configurations.AddFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<AdminUserEntity> AdminUsers { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<CityEntity> Cities { get; set; }
        public DbSet<CommunityEntity> Communities { get; set; }
        public DbSet<PermissionEntity> Permissions { get; set; }
        public DbSet<RegionEntity> Regions { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<SettingEntity> Settings { get; set; }
        public DbSet<AttachmentEntity> Attachments { get; set; }
        public DbSet<HouseEntity> Houses { get; set; }
        public DbSet<HouseAppointmentEntity> HouseAppointments { get; set; }
        public DbSet<IdNameEntity> IdNames { get; set; }
        public DbSet<HousepicEntity> HousePics { get; set; }
        public DbSet<AdminLogEntity> AdminUserLogs { get; set; }
    }

}
