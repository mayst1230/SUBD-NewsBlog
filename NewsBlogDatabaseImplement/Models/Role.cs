using System;
using System.Collections.Generic;

namespace NewsBlogDatabaseImplement.Models
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<Users>();
        }

        public int Idrole { get; set; }
        public string Namerole { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
