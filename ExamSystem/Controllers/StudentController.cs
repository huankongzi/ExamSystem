using ExamSystem.AuthInterfaces;
using ExamSystem.Domain;
using ExamSystem.Dto;
using ExamSystem.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController(
    IStudentCreator studentCreator,
    IStudentOrder studentOrder,
    ILogger<StudentController> logger)
    : Controller
{
    /// <summary>
    ///     generate a roster with the given number of students
    /// </summary>
    /// <param name="request">the request include numbers of students</param>
    /// <returns>generated students</returns>
    [HttpGet("generate/{number}")]
    public Response<List<Student>> GenerateRoster(GenerateRequest request)
    {
        logger.LogInformation("Generating roster with {number} students", request.Number);

        var roster = new Roster(studentCreator, studentOrder);
        roster.Generate(request.Number);
        logger.LogInformation(
            $"Generating roster with {request.Number} students, the ids are {string.Join(',', roster.Select(r => r.Id.ToString()))}");

        roster.Order();
        logger.LogInformation(
            $"After ordered, the ids are {string.Join(',', roster.Select(r => r.Id.ToString()))}");

        return new Response<List<Student>> { Result = roster.ToList() };
    }

    /// <summary>
    ///     A dummy method to test error handling
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet("error")]
    public Response<List<Student>> Error()
    {
        throw new NotImplementedException("Just for testing error handling");
    }
}