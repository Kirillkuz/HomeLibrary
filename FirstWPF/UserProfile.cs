using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstWPF
{
    public class UserProfile
    {
        [Key]
        [ForeignKey("LoginInfo")]
        public int Id { get; set; }

        public string Name { get; set; }
        public int Age { get; set; }

        public LoginInfo LoginInfo { get; set; }

        public virtual ICollection<Book> Books { get; set; }
        public UserProfile()
        {
            Books = new List<Book>();
        }
    }
}
