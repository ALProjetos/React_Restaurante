using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ApiRestaurante
{
    public class ValidationFilterAttribute : IActionFilter
    {
        public void OnActionExecuting( ActionExecutingContext context )
        {
            // Verifica os campos obrigatórios do model
            if ( !context.ModelState.IsValid )
            {
                context.Result = new BadRequestObjectResult( context.ModelState );
            }
        }

        public void OnActionExecuted( ActionExecutedContext context )
        {
        }
    }
}
