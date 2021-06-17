namespace MailSender.TestWPF.Models
{
    public class Student
    {
        public string Name { get; set; }

        public string Group { get; set; }

        public int Age { get; set; }

        public double Rating { get; set; }

        public override string ToString() => $"Студент {Name} ({Group})";

        public override bool Equals(object? obj)
        {
            if (obj is not Student other) return false;
            return Name == other.Name && Group == other.Group && Age == other.Age;
        }

        public override int GetHashCode()
        {
            var hash = 13;
            unchecked
            {
                if (Name != null)
                    hash = (hash ^ Name.GetHashCode()) * 13;
                if (Group != null)
                    hash = (hash ^ Group.GetHashCode()) * 13;
                hash = (hash ^ Age.GetHashCode()) * 13;
            }

            return hash;
        }
    }
}
