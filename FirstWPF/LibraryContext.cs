using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace FirstWPF
{
    class LibraryContext : DbContext
    {
        public LibraryContext()
            : base("DbConnection")
        { }

        public DbSet<LoginInfo> LoginInfos { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }

    }
}
