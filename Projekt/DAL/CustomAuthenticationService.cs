 using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt.DAL
{
    public class CustomAuthenticationService 
    {
        private BlogContext db;

        public CustomAuthenticationService(BlogContext db)
        {
            this.db = db;
        }

        public bool PersonExists(string Email)
        {
            try
            {
               var result = GetPersonByEmail(Email);
               return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            
        }

        public ICollection<Comment> getPersonComments(Person person)
        {
            try
            {
                var result = (from comment in db.Comments
                              where comment.CommentWriter == person
                              select comment).ToList<Comment>();
                return result;
            }catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public bool isValid(string Email, string Password)
        {
            if (PersonExists(Email))
                if (GetPersonByEmail(Email).Password == Password)
                    return true;
                return false;
            

        }

        public Person GetPersonByEmail(string Email)
        {
            return db.People.Single(p => p.Email == Email);     
        }

        public Person GetPersonByNick(string Nickname)
        {
            return db.People.Single(p => p.Nickname == Nickname);
        }

        public Person GetPerson(long personId)
        {
            return db.People.Single(p => p.ID == personId);
        }

        public bool UpdatePerson(Person person)
        {
            var original = db.People.Find(person.ID);

            if (original != null)
            {
                db.Entry(original).CurrentValues.SetValues(person);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool NicknameValid(string nickname)
        {
            try
            {
                var person = db.People.Single(p => p.Nickname == nickname);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return true;
            }
        }
    }
}