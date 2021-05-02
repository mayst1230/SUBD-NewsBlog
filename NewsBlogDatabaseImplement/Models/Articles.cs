using System;
using System.Collections.Generic;

namespace NewsBlogDatabaseImplement.Models
{
    public partial class Articles
    {
        public Articles()
        {
            Comments = new HashSet<Comments>();
        }

        public int Idarticle { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime Date { get; set; }
        public int Idtheme { get; set; }
        public int Iduser { get; set; }

        public virtual Categories IdthemeNavigation { get; set; }
        public virtual Users IduserNavigation { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
    }
}
