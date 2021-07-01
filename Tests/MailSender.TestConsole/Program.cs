using System;
using System.Linq;
using System.Threading.Tasks;

using MailSender.TestConsole.Context;
using MailSender.TestConsole.Entityes;

using Microsoft.EntityFrameworkCore;

namespace MailSender.TestConsole
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<StudentsDB>()
               .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Students.db")
               //.LogTo(s => Console.WriteLine("EF >>> {0}", s))
               .Options;

            await using (var db = new StudentsDB(options))
            {
                await InitializeDbAsync(db).ConfigureAwait(false);
            }

            await using (var db = new StudentsDB(options))
            {
                var best_students_count = await db.Students
                   .CountAsync(s => s.Rating > 75);

                var best_courses = await db.Courses
                   .Where(c => c.Students.Average(s => s.Rating) > 50)
                   .CountAsync();

                var course_ratings = await db.Courses
                   .Select(c => new { c.Name, AverageRating = c.Students.Average(s => s.Rating) })
                   .ToArrayAsync();

                var best_course = await db.Courses
                   .Include(c => c.Students)
                   //.ThenInclude(s => s.Courses)
                   .OrderByDescending(c => c.Students.Average(s => s.Rating))
                   .FirstOrDefaultAsync();
            }

            Console.WriteLine("Работа завершена. Нажмите Enter для выхода");
            Console.ReadLine();
        }

        private static async Task InitializeDbAsync(StudentsDB db)
        {
            //await db.Database.EnsureDeletedAsync().ConfigureAwait(false);
            //await db.Database.EnsureCreatedAsync().ConfigureAwait(false);

            await db.Database.MigrateAsync().ConfigureAwait(false);

            //Console.Clear();
            if (await db.Students.AnyAsync() || await db.Courses.AnyAsync()) return;

            await using var transaction = await db.Database.BeginTransactionAsync();

            var courses = Enumerable.Range(1, 10)
               .Select(i => new Course { Name = $"Курс лекций {i}" })
               .ToArray();

            await db.Courses.AddRangeAsync(courses);
            await db.SaveChangesAsync();

            var rnd = new Random();

            var students = Enumerable.Range(1, 1000)
               .Select(i => new Student
               {
                   Name = $"Имя-{i}",
                   LastName = $"Фамилия-{i}",
                   Patronymic = $"Отчество-{i}",
                   Birthday = DateTime.Now.Date.AddYears(-rnd.Next(17, 28)).AddDays(rnd.Next(365)),
                   Courses = Enumerable.Range(0, rnd.Next(1, 8))
                      .Select(_ => courses[rnd.Next(courses.Length)])
                      .Distinct()
                      .ToArray(),
                   Rating = rnd.NextDouble() * 100
               });

            await db.Students.AddRangeAsync(students).ConfigureAwait(false);
            await db.SaveChangesAsync().ConfigureAwait(false);

            await transaction.CommitAsync().ConfigureAwait(false);
        }
    }
}
