using System;
using System.Collections.Generic;

namespace NewsBlogDatabaseImplement.Models
{
    public partial class Users
    {
        public Users()
        {
            Articles = new HashSet<Articles>();
            Comments = new HashSet<Comments>();
        }

        public int Iduser { get; set; }
        public string Nickname { get; set; }
        public int Idrole { get; set; }

        public virtual Role IdroleNavigation { get; set; }
        public virtual ICollection<Articles> Articles { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
    }
}
