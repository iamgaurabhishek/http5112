using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPassionProjectW2024n01605783.Models
{
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }

        public string BlogHeading { get; set; }
        public string BlogContent { get; set; }
        public string BlogShortDescription { get; set; }

        public string BlogFeaturedImageUrl { get; set; }

        public DateTime BlogPublishedDate { get; set; }
        public string BlogAuthor { get; set; }

        //A BlogPost can public string BlogAuthor { get; set; }have many Tags
        //public ICollection<Tag> Tags { get; set; }

        //A BlogPost can have many Comments
        //public ICollection<Comment> Comments { get; set; }
        [ForeignKey("Tag")]
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }

        [ForeignKey("Comment")]
        public int CommentId { get; set; }
        public virtual Comment Comment { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }

    public class BlogDto
    {
        public int BlogId { get; set; }

        public string BlogHeading { get; set; }

        public string BlogContent { get; set; }

        public string BlogShortDescription { get; set; }

        public string BlogAuthor { get; set; }

        public string BlogFeaturedImageUrl { get; set; }

        public DateTime BlogPublishedDate { get; set; }

        //public ICollection<Tag> Tags { get; set; }
        //public ICollection<Comment> Comments { get; set; }

        // *******************************BLOG------------------------------
        public int TagId { get; set; }
        public string TagName { get; set; }
        //-----------------------COMMENT***********************************
        public int CommentId { get; set; }
        public string CommentDescription { get; set; }

        // *************************USER-----------------------------
        public string UserName { get; set; }
        public int UserId { get; set; }
    }
}