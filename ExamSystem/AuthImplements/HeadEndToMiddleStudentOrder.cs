using ExamSystem.AuthInterfaces;
using ExamSystem.Entities;

namespace ExamSystem.AuthImplements;

public class HeadEndToMiddleStudentOrder : IStudentOrder
{
    public IEnumerable<Student> Order(List<Student> students)
    {
        if (students.Count == 0) return new List<Student>();

        var start = 0;
        var end = students.Count - 1;
        var orderedStudents = new List<Student>();
        while (start < end)
        {
            orderedStudents.Add(students[start++]);
            orderedStudents.Add(students[end--]);
        }

        if (start == end) orderedStudents.Add(students[start]);

        return orderedStudents;
    }
}