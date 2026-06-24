using Application.Services.Abstract.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MainApp.Filters;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
public class RequireAdmin : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var currentUserService = context.HttpContext.RequestServices.GetRequiredService<ICurrentUserService>();
        
        if (!currentUserService.IsAdmin)
        {
            context.Result = new ForbidResult();
            return;
        }
    }
}
