using ExamSystem.Dto;
using FluentValidation;

namespace ExamSystem.Validators;

public class GenerateRequestValidator : AbstractValidator<GenerateRequest>
{
    public GenerateRequestValidator()
    {
        RuleFor(x => x.Number).GreaterThan(20);
    }
}