namespace ExamSystem.Dto;

public class Response<T>
{
    /// <summary>
    /// the result of the response
    /// </summary>
    public T? Result { get; set; }

    /// <summary>
    /// the error message of the response
    /// </summary>
    public string? ErrorMessage { get; set; }
}