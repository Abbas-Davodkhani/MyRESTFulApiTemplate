using Microsoft.EntityFrameworkCore;
using RESTFulApi.Template.Models.Entities;

namespace RESTFulApi.Template.Models.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDo>().HasQueryFilter(p => !p.IsRemove);
            // base.OnModelCreating(modelBuilder);
        }
    }
}
