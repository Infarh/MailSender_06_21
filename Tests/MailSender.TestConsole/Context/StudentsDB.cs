using MailSender.TestConsole.Entityes;
using Microsoft.EntityFrameworkCore;

namespace MailSender.TestConsole.Context
{
    public class StudentsDB : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

        public StudentsDB(DbContextOptions<StudentsDB> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);

            model.Entity<Student>()
               .Property(s => s.Name)
               .HasMaxLength(150)
               .IsRequired();

            //model.Entity<Student>()
            //   .HasMany(s => s.Courses)
            //   .WithMany(c => c.Students);
        }
    }
}
