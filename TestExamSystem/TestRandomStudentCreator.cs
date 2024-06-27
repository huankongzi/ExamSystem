using ExamSystem.AuthImplements;

namespace TestExamSystem;

public class TestRandomStudentCreator
{
    [Test]
    public void Should_ThrowException_When_Generate_GivenNumberIsNegetive()
    {
        // Arrange
        var randomStudentCreator = new RandomStudentCreator();
        var number = -1;

        // Act
        var exception = Assert.Throws<ArgumentException>(() => randomStudentCreator.Generate(number));

        // Assert
        Assert.That(exception.Message, Is.EqualTo("Number of students must be positive and greater than 20"));
    }

    [Test]
    public void Should_ThrowException_When_Generate_GivenNumberIsLessThan20()
    {
        // Arrange
        var randomStudentCreator = new RandomStudentCreator();
        var number = 10;

        // Act
        var exception = Assert.Throws<ArgumentException>(() => randomStudentCreator.Generate(number));

        // Assert
        Assert.That(exception.Message, Is.EqualTo("Number of students must be greater than 20"));
    }

    [Test]
    public void Should_ReturnStudents_When_Generate_GivenNumberIsGreaterThan20()
    {
        // Arrange
        var randomStudentCreator = new RandomStudentCreator();
        var number = 30;

        // Act
        var students = randomStudentCreator.Generate(number);

        // Assert
        Assert.That(students.Count, Is.EqualTo(number));
        Assert.That(students[0].Index, Is.EqualTo(1));
    }
}