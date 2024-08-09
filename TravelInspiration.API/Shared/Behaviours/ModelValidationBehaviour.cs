using FluentValidation;
using MediatR;

namespace TravelInspiration.API.Shared.Behaviours;

public sealed class ModelValidationBehaviour<TRequest, IResult>(
    IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, IResult> where TRequest : IRequest<IResult>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators = validators;

    public async Task<IResult> Handle(TRequest request, 
        RequestHandlerDelegate<IResult> next, 
        CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);
        var validationResults = _validators
            .Select(v => v.Validate(context))
            .ToList();
        var groupedErrors = validationResults
            .SelectMany(r => r.Errors)
            .GroupBy(g => g.PropertyName)
            .ToDictionary(g => g.Key, g => g.Select(e => e.ErrorMessage).ToArray());

        return groupedErrors.Any() 
            ? (IResult)Results.ValidationProblem(groupedErrors) 
            : await next();
    }
}
