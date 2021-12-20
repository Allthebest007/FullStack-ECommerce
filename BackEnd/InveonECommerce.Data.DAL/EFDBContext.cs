using InveonECommerce.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Data.DAL
{
    public class EFDBContext : IdentityDbContext<UserEntity,IdentityRole,string>
    {
        public EFDBContext(DbContextOptions<EFDBContext> options ) :base (options)
        {

        }

        

        private static void UseAsEntity(ModelBuilder modelBuilder, Type type)
        {
            modelBuilder.Entity(type);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Type baseEntityType = typeof(BaseEntity);
            Assembly assembly = baseEntityType.Assembly;
            Type[] allTypes = assembly.GetTypes();
            var entities = allTypes.Where(q => q.BaseType == baseEntityType && q != baseEntityType).ToList();
            foreach (var entityType in entities)
            {
                UseAsEntity(modelBuilder, entityType);
            }
            base.OnModelCreating(modelBuilder);

            //Seeding a  'Admin' and 'Customer' role to AspNetRoles table
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole {
                Id = "2c5e174e-3b0e-446f-86af-483d56fd7210", 
                Name = "Admin", 
                NormalizedName = "ADMIN".ToUpper() },
                new IdentityRole
                {
                    Id = "c189c648-c905-4c83-a1c9-5fbb0ce8db7c",
                    Name = "Customer",
                    NormalizedName = "CUSTOMER".ToUpper()
                }
                );


            //a hasher to hash the password before seeding the user to the db
            var hasher = new PasswordHasher<UserEntity>();


            //Seeding the User to AspNetUsers table
            modelBuilder.Entity<UserEntity>().HasData(
                new UserEntity
                {
                    Id = "8e445865-a24d-4543-a6c6-9443d048cdb9", // primary key
                    UserName = "Emreadmin",
                    NormalizedUserName = "EMREADMIN",
                    PasswordHash = hasher.HashPassword(null, "Emrekurtar007!")
                }
            );


            //Seeding the relation between our user and role to AspNetUserRoles table
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "2c5e174e-3b0e-446f-86af-483d56fd7210",
                    UserId = "8e445865-a24d-4543-a6c6-9443d048cdb9"
                }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }


    }
}
