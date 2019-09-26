using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace owned_bug
{
    public class DueDate
    {
        private DueDate() { }

        public DueDate(DateTimeOffset date)
        {
            Date = date;
        }
        
        public DateTimeOffset? Date { get; private set; }
    }
    
    public class Task
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public DueDate DueDate { get; set; }
    }

    public class TasksContext : DbContext
    {
        public DbSet<Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            base.OnConfiguring(options);
            options.UseSqlite("Data Source=tasks.db");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Task>().OwnsOne(x => x.DueDate);
        }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            // This is ok
            using (var db = new TasksContext())
            {
                // CleanUp
                var old = db.Tasks.ToList();
                db.Tasks.RemoveRange(old);
                
                // Create a task with a due date
                db.Tasks.Add(new Task()
                {
                    Name = "A task.",
                    DueDate = new DueDate(DateTimeOffset.Now)
                });
                db.SaveChanges();

                // Load the task
                var task = db.Tasks.First();
                
                // Replace the due date
                task.DueDate = new DueDate(DateTimeOffset.Now);
                db.SaveChanges();
            }
            
            // This is not ok
            using (var db = new TasksContext())
            {
                // CleanUp
                var old = db.Tasks.ToList();
                db.Tasks.RemoveRange(old);
                
                // Create a task without a due date
                db.Tasks.Add(new Task()
                {
                    Name = "A task."
                });
                db.SaveChanges();

                // Load the task
                var task = db.Tasks.First();
                
                // Update the due date
                task.DueDate = new DueDate(DateTimeOffset.Now);
                db.SaveChanges();
            }

            Console.ReadKey();
        }
    }
}
