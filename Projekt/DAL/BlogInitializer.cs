using Projekt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Projekt.DAL
{
    public class BlogInitializer:System.Data.Entity.DropCreateDatabaseAlways<BlogContext>
    {
        protected override void Seed(BlogContext context)
        {
            var people = new List<Person>
            {
                new Person {Nickname="Serafin6969",Email="serafin6969@gmail.com",Password="Admin1111/",Role=Person.Roles.Administrator},
                new Person {Nickname="AK47", Email="adasioPL@gmail.com", Password="Admin1111/",Role=Person.Roles.Writer}
            };

            people.ForEach(r => context.People.Add(r));
            context.SaveChanges();

            var posts = new List<Post>
            {
                new Post {PostWriter=people.Single(i=>i.Nickname=="Serafin6969"),Title="Pierwszy post",Content="Witajcie na blogu dziubaski" },
                new Post {PostWriter=people.Single(i=>i.Nickname=="Serafin6969"),Title="drugi post", Content="witajcie po raz drugi" }
            };

            posts.ForEach(p => context.Posts.Add(p));
            context.SaveChanges();

            var comments = new List<Comment>
            {
                new Comment {CommentWriter=people.Single(p=>p.Nickname=="Serafin6969"),Post=posts.Single(i=>i.PostID==1),CommentContent="Hehe, to ja serafin" },
                new Comment {CommentWriter=people.Single(p=>p.Nickname=="AK47"),Post=posts.Single(i=>i.PostID==1),CommentContent="drugi koment"}
            };

            comments.ForEach(c => context.Comments.Add(c));
            context.SaveChanges();

            context.People.Single(p => p.Nickname == "Serafin6969").Comments.Add(context.Comments.Single(c => c.CommentID == 1));
            context.SaveChanges();
        }
    }
}