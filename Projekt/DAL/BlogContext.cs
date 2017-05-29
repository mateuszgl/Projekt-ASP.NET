using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Projekt.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Projekt.DAL
{
    public class BlogContext: DbContext
    {
        public BlogContext():base("Blog Context") { }
        public DbSet<Person> People { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }

        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            /*modelBuilder.Entity<Post>()
             .HasRequired(c => c.PostWriter).WithMany(i => i.Posts)
             .Map(t => {
                 t.MapKey("PostID");
                 t.MapKey("PostWriterID");
                 t.ToTable("PostWriter");
              });

            modelBuilder.Entity<Comment>()
             .HasRequired(c => c.CommentWriter).WithMany(i => i.Comments)
             .Map(t => {
                 t.MapKey("CommentID");
                 t.MapKey("CommentWriterID");
                 t.ToTable("CommentWriter");
             });

            modelBuilder.Entity<Comment>()
             .HasRequired(c => c.Post).WithMany(i => i.Comments)
             .Map(t => {
                 t.MapKey("CommentID");
                 t.MapKey("PostID");
                 t.ToTable("PostComment");
             });
             */
        }
    }
}