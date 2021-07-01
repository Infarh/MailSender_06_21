using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MailSender.TestConsole.Entityes
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public string Description { get; set; }

        //public Student Student { get; set; }

        public ICollection<Student> Students { get; set; } = new HashSet<Student>();
    }
}
