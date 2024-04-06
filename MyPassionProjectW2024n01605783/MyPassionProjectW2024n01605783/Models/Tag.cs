using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPassionProjectW2024n01605783.Models
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        public string TagName { get; set; }

        //A Tag can be on many BlosPosts
        //[ForeignKey("Blog")]
        //public int BlogId { get; set; }

        //public virtual Blog Blog { get; set; }
    }
    public class TagDto
    {
        public int TagId { get; set; }
        public string TagName { get; set; }

        // Blog
        //public int BlogId { get; set; }

    }
}