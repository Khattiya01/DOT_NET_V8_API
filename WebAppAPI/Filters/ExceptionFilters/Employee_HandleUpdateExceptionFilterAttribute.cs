using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAppAPI.Filters.ExceptionFilters
{
    public class Employee_HandleUpdateExceptionFilterAttribute: ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);

            var strEmployeeId = context.RouteData.Values["id"] as string;
            if ((int.TryParse(strEmployeeId, out int employeeId)))
            {
                context.ModelState.AddModelError("Employee", "Employee doesn't exist anymore.");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status404NotFound
                };
                context.Result = new NotFoundObjectResult(problemDetails);
            }
        }
    }
}
