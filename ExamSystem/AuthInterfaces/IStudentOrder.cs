using ExamSystem.Entities;

namespace ExamSystem.AuthInterfaces;

public interface IStudentOrder
{
    IEnumerable<Student> Order(List<Student> students);
}