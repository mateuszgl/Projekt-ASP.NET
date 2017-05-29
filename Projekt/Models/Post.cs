using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Projekt.Models
{
    public class Post
    {
        [DisplayName("Post ID")]
        public int PostID { get; set; }
        
        [Required]
        [StringLength(128, ErrorMessage ="Title cannot have more than 128 characters")]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(1024, ErrorMessage = "Content cannot have more than 1024 characters")]
        public string Content { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        [DisplayName("Author")]
        public virtual Person PostWriter { get; set; }
    }
}