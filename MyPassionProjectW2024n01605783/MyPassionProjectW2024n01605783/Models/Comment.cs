using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPassionProjectW2024n01605783.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        public string CommentDescription { get; set; }

        // Foreign key property
        //[ForeignKey("Blog")]
        //public int BlogId { get; set; }
        //public virtual Blog Blog { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

    }
    public class CommentDto
    {
        public int CommentId { get; set; }
        public string CommentDescription { get; set; }
        //public int BlogId { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }

    }
}