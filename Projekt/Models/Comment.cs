using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Projekt.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        
        [Required]
        [DisplayName("Comment Content")]
        [DataType(DataType.MultilineText)]
        [StringLength(255,ErrorMessage ="Comment cannot have more than 255 characters")]
        public string CommentContent { get; set; }
        
        public virtual Post Post { get; set; }
        public virtual Person CommentWriter { get; set; }

    }
}