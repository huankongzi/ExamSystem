using ExamSystem.Entities;

namespace ExamSystem.AuthInterfaces;

public interface IStudentCreator
{
    List<Student> Generate(int number);
}