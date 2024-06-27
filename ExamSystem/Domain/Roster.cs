using System.Collections;
using ExamSystem.AuthInterfaces;
using ExamSystem.Entities;

namespace ExamSystem.Domain;

public class Roster(IStudentCreator studentCreator, IStudentOrder studentOrder)
    : IEnumerable<Student>
{
    private List<Student>? _students;

    public IEnumerator<Student> GetEnumerator()
    {
        return _students?.GetEnumerator() ?? Enumerable.Empty<Student>().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Generate(int number)
    {
        _students = studentCreator.Generate(number);
    }

    public void Order()
    {
        if (_students != null) _students = studentOrder.Order(_students).ToList();
    }
}