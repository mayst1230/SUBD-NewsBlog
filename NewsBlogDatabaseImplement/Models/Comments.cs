using System;
using System.Collections.Generic;

namespace NewsBlogDatabaseImplement.Models
{
    public partial class Comments
    {
        public int Idcomment { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public int Idarticle { get; set; }
        public int Iduser { get; set; }

        public virtual Articles IdarticleNavigation { get; set; }
        public virtual Users IduserNavigation { get; set; }
    }
}
