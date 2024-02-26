using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Autopark.Areas.Manager.Authorization
{
    public class ManagedCompaniesFilter : Attribute, IAsyncResourceFilter
    {
        public Task OnResourceExecutionAsync(ResourceExecutingContext context, ResourceExecutionDelegate next)
        {
            var path = context.HttpContext.Request.Path.Value;
            if (path == null) context.Result = new BadRequestResult();

            context.HttpContext.User.Claims.First();
            throw new MissingMethodException();
        }
    }
}
