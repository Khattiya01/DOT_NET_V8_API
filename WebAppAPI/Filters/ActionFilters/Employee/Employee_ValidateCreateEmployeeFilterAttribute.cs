using Microsoft.AspNetCore.Mvc.Filters;
using WebAppAPI.Models.Repositorys;
using WebAppAPI.Models;
using Microsoft.AspNetCore.Mvc;
using WebAppAPI.Data;
using System.Drawing;
using System.Reflection;

namespace WebAppAPI.Filters.ActionFilters
{
    public class Employee_ValidateCreateEmployeeFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var employee = context.ActionArguments["employee"] as Employee;

            if (employee == null)
            {
                context.ModelState.AddModelError("Employee", "Employee object is null");
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest
                };
                context.Result = new BadRequestObjectResult(problemDetails);
            }
           
        }
    }
}
