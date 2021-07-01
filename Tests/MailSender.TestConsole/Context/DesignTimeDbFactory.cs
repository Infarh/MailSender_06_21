using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MailSender.TestConsole.Context
{
    public class DesignTimeDbFactory : IDesignTimeDbContextFactory<StudentsDB>
    {
        public StudentsDB CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<StudentsDB>()
               .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Students.db")
               .Options;

            return new StudentsDB(options);
        }
    }
}
