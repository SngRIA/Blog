using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Blog.Models
{
    public class BlogContext : DbContext
    {
        public BlogContext() 
            /*: base("DefaultConnection") */
        {
            DefaultArticle = new Article { 
                Id = 0, 
                Title = "Не найдено", 
                Description = "Не найдено", 
                Text = "Не найдено", 
                Views = 0
            };

            DefaultUser = new User { 
                Id = 0, 
                Email = "Не найдено", 
                FirstName = "Не найдено", 
                MiddleName = "Не найдено", 
                SecondName = "Не найдено", 
                Password = "Не найдено" 
            };
        }
        public DbSet<Article> Articles { get; set; }
        public Article DefaultArticle;
        public DbSet<User> Users { get; set; }
        public User DefaultUser;
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>(); // Удаляем множественное число у таблиц в бд
        }
    }

    public class BlogDbInitializer : DropCreateDatabaseAlways<BlogContext>
    {
        protected override void Seed(BlogContext context)
        {
            foreach (var article in GenArticles(1, 8))
            {
                context.Articles.Add(article);
            }

            context.Users.Add(new User
            {
                Id = 1,
                FirstName = "Name",
                MiddleName = "Name",
                SecondName = "Name",
                Email = "email@email.email",
                Password = "password" // Best security
            });

            context.SaveChanges();
        }

        private List<Article> GenArticles(int startId, int count)
        {
            List<Article> articles = new List<Article>();

            for (int i = startId; i < count; i++)
            {
                articles.Add(new Models.Article
                {
                    Id = i,
                    Title = "Title" + i,
                    Description = "Desc" + i,
                    Text = "Text" + i,
                    Views = 1,
                    Date = DateTime.Now
                });
            }

            return articles;
        }
    }
}