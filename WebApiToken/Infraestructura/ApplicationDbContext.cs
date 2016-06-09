using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiToken.Infraestructura
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base(GetEfConnectionString(), throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        
        static string BaseDeDatos = ConfigurationManager.AppSettings["BaseDeDatos"];

        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //http://stackoverflow.com/questions/28878718/asp-net-mvc5-keeping-users-in-oracle-database
            
            if (BaseDeDatos == "Oracle")
            {
                modelBuilder.HasDefaultSchema("ASPNETIDENTITY");
                modelBuilder.Entity<ApplicationUser>().ToTable("AspNetUsers");
                modelBuilder.Entity<IdentityRole>().ToTable("AspNetRoles");
                modelBuilder.Entity<IdentityUserRole>().ToTable("AspNetUserRoles");
                modelBuilder.Entity<IdentityUserClaim>().ToTable("AspNetUserClaims");
                modelBuilder.Entity<IdentityUserLogin>().ToTable("AspNetUserLogins");
            }

        }

        private static string GetEfConnectionString()
            {
                if (BaseDeDatos == "MySql")
                {
                    return "DefaultConnectionMySql";
                }
                else
                if (BaseDeDatos == "Oracle")
                {
                    return "DefaultConnectionOracle";
                }
                else
                {
                    return "DefaultConnectionSqlServer";
                }
            }
        

    }
}
