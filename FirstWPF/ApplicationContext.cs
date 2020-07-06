using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FirstWPF
{
    public class ApplicationContext : DbContext
    {
        public DbSet<LoginInfo> LoginInfos { get; set; }

        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=000webhost.com;UserId=id14280353_admin;Password=2c4Jt@v{rmsp*WOw;database=id14280353_homelibrary;");
        }
    }
}