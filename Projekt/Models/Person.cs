using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Projekt.Models
{

    public class Person
    {
        public int ID { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(18, ErrorMessage = "Nickname name cannot be longer than 18 characters.")]
        public string Nickname { get; set; }
   
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Post> Posts { get; set; }

       
        [EnumDataType(typeof(Roles))]
        public Roles Role { get; set; }

        public enum Roles
        {
            Reader = 0,
            Writer = 1,
            Administrator = 2,
        }

    }
}