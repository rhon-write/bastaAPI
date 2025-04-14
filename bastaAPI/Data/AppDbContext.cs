using bastaAPI.Models;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace bastaAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<ToDoItem> ToDoItems => Set<ToDoItem>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<SubTask> SubTasks => Set<SubTask>();
    }
}
