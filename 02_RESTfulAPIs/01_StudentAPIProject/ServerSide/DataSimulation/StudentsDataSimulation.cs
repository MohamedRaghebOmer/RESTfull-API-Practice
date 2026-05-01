using StudentAPI.Models;

namespace StudentAPI.DataSimulation
{
    public class StudentsDataSimulation
    {
        public static readonly List<Student> StudentsList = new List<Student>()
        {
            new Student { Id = 1, Name = "Mohamed", Age = 18, Grade = 100 },
            new Student { Id = 2, Name = "Ahmed", Age = 23, Grade = 23 },
            new Student { Id = 3, Name = "Ali", Age = 20, Grade = 90 },
            new Student { Id = 4, Name = "Fadi", Age = 27, Grade = 30 },
            new Student { Id = 5, Name = "Menna", Age = 21, Grade = 99 }
        };
    }
}
