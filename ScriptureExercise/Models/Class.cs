using Microsoft.AspNetCore.Mvc.Filters;

namespace ScriptureExercise.Models
{
    public class Class : IAuthorizationFilter

    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            //context.Result = 
        }
    }
}
