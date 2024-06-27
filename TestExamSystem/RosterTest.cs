using ExamSystem.AuthInterfaces;
using ExamSystem.Domain;
using ExamSystem.Entities;
using Moq;

namespace TestExamSystem;

public class RosterTest
{
    private Mock<IStudentCreator> _studentCreatorMock;
    private Mock<IStudentOrder> _studentOrderMock;

    [Test]
    public void Should_ReturnEmptyList_When_Order_GivenEmptyList()
    {
        // Arrange
        _studentCreatorMock = new Mock<IStudentCreator>();
        _studentOrderMock = new Mock<IStudentOrder>();
        var roster = new Roster(_studentCreatorMock.Object, _studentOrderMock.Object);

        // Act
        roster.Order();

        // Assert
        Assert.That(roster, Is.Empty);
    }

    [Test]
    public void Should_ReturnOrderedStudents_WhenGetEnumerator_Order_GivenStudents()
    {
        // Arrange
        _studentCreatorMock = new Mock<IStudentCreator>();
        _studentOrderMock = new Mock<IStudentOrder>();
        var roster = new Roster(_studentCreatorMock.Object, _studentOrderMock.Object);
        var students = new List<Student>
        {
            new() { Index = 1, Id = 1 },
            new() { Index = 2, Id = 2 },
            new() { Index = 3, Id = 3 },
            new() { Index = 4, Id = 4 },
            new() { Index = 5, Id = 5 }
        };
        _studentCreatorMock.Setup(x => x.Generate(It.IsAny<int>())).Returns(students);
        _studentOrderMock.Setup(x => x.Order(students)).Returns(students);

        // Act
        roster.Generate(5);
        roster.Order();

        // Assert
        Assert.That(roster.Count, Is.EqualTo(5));
    }
}