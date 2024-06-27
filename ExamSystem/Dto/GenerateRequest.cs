using Microsoft.AspNetCore.Mvc;

namespace ExamSystem.Dto;

public class GenerateRequest
{
    /// <summary>
    /// the student number to generate
    /// </summary>
    [FromRoute(Name = "number")] public int Number { get; set; }
}