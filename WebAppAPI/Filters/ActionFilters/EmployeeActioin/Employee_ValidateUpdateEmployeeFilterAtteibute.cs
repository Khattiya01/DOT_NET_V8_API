using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAppAPI.Models;

namespace WebAppAPI.Filters.ActionFilters
{
    public class Employee_ValidateUpdateEmployeeFilterAtteibute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var id = context.ActionArguments["id"] as Guid?;
            var employee = context.ActionArguments["employee"] as Employee;
            if (id.HasValue && employee != null && id != employee.UserId)
            {
                context.ModelState.AddModelError("Employee", "Employee is not same as id");
                var ploblemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "detail"
                };
                context.Result = new BadRequestObjectResult(ploblemDetails);
            }
        }
    }
}
