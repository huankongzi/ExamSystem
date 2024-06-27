using System.Security.Cryptography;
using ExamSystem.AuthInterfaces;
using ExamSystem.Entities;

namespace ExamSystem.AuthImplements;

public class RandomStudentCreator : IStudentCreator
{
    private readonly HashSet<int> _idSet = new();
    private readonly RandomNumberGenerator _random = RandomNumberGenerator.Create();

    public List<Student> Generate(int number)
    {
        if (number <= 0) throw new ArgumentException("Number of students must be positive and greater than 20");

        if (number <= 20) throw new ArgumentException("Number of students must be greater than 20");

        var students = new List<Student>();

        for (var i = 0; i < number; i++)
        {
            var id = GetNextId();
            while (_idSet.Contains(id)) id = GetNextId();

            _idSet.Add(id);
            students.Add(new Student { Index = i + 1, Id = id });
        }

        return students;
    }

    private int GetNextId()
    {
        var buffer = new byte[8];
        _random.GetBytes(buffer);
        var id = BitConverter.ToInt32(buffer, 0);
        return Math.Abs(id);
    }
}