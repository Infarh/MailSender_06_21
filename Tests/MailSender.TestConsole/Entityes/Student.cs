using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailSender.TestConsole.Entityes
{
    public class Student
    {
        public int Id { get; set; }
        //public long Id { get; set; }
        //public string Id { get; set; }
        //public int StudentId { get; set; }
        //public Guid StudentId { get; set; }
        //[Key] public int OtherPrimaryKey { get; set; }

        //[Required]
        //[MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string Patronymic { get; set; }

        public DateTime? Birthday { get; set; }

        public double? Rating { get; set; }

        [MaxLength(500)]
        public string Comment { get; set; }

        public ICollection<Course> Courses { get; set; } = new HashSet<Course>();
    }
}
