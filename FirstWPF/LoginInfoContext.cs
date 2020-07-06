using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace FirstWPF
{
    class LoginInfoContext : DbContext
    {
        public LoginInfoContext()
            : base("DbConnection")
        { }

        public DbSet<LoginInfo> LoginInfos { get; set; }
    }
}
