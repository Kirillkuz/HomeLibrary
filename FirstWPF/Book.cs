using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstWPF
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }

        public virtual ICollection<UserProfile> UserProfiles { get; set; }
        public Book()
        {
            UserProfiles = new List<UserProfile>();
        }

    }
}
