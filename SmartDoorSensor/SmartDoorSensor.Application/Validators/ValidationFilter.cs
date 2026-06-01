using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.Validators;

public class ValidationFilter<T> : IAsyncActionFilter where T : class
{
    private readonly IValidator<T> _validator;

    public ValidationFilter(IValidator<T> validator)
    {
        _validator = validator;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var argument = context.ActionArguments.Values
            .OfType<T>()
            .FirstOrDefault();

        if (argument is not null)
        {
            var result = await _validator.ValidateAsync(argument);
            if (!result.IsValid)
            {
                context.Result = new BadRequestObjectResult(result.Errors.Select(e => e.ErrorMessage));
                return;
            }
        }

        await next();
    }
}
