using ExamSystem.AuthImplements;
using ExamSystem.Entities;

namespace TestExamSystem;

public class TestHeadEndToMiddleStudentOrder
{
    [Test]
    public void Should_ReturnEmptyList_When_Order_GivenEmptyList()
    {
        // Arrange
        var headEndToMiddleStudentOrder = new HeadEndToMiddleStudentOrder();
        var students = new List<Student>();

        // Act
        var orderedStudents = headEndToMiddleStudentOrder.Order(students);

        // Assert
        Assert.That(orderedStudents, Is.Empty);
    }

    [Test]
    public void Should_ReturnOrderedStudents_When_Order_GivenStudents()
    {
        // Arrange
        var headEndToMiddleStudentOrder = new HeadEndToMiddleStudentOrder();
        var students = new List<Student>
        {
            new() { Index = 1, Id = 1 },
            new() { Index = 2, Id = 2 },
            new() { Index = 3, Id = 3 },
            new() { Index = 4, Id = 4 },
            new() { Index = 5, Id = 5 }
        };

        // Act
        var orderedStudents = headEndToMiddleStudentOrder.Order(students).ToArray();

        // Assert
        Assert.That(orderedStudents.Count, Is.EqualTo(5));
        Assert.That(orderedStudents[0].Index, Is.EqualTo(1));
        Assert.That(orderedStudents[1].Index, Is.EqualTo(5));
        Assert.That(orderedStudents[2].Index, Is.EqualTo(2));
        Assert.That(orderedStudents[3].Index, Is.EqualTo(4));
        Assert.That(orderedStudents[4].Index, Is.EqualTo(3));
    }
}