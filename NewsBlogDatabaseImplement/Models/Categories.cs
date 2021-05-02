using System;
using System.Collections.Generic;

namespace NewsBlogDatabaseImplement.Models
{
    public partial class Categories
    {
        public Categories()
        {
            Articles = new HashSet<Articles>();
        }

        public int Idtheme { get; set; }
        public string Nametheme { get; set; }

        public virtual ICollection<Articles> Articles { get; set; }
    }
}
