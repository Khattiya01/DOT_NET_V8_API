using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using WebAppAPI.Data;
using WebAppAPI.Models;
using WebAppAPI.Models.Repositorys;

namespace WebAppAPI.Filters.ActionFilters
{
    public class Employee_ValidateEmployeeIdIdFilterAttribute : ActionFilterAttribute
    {
        private readonly DataContext _context;

        public Employee_ValidateEmployeeIdIdFilterAttribute(DataContext context)
        {
            _context = context;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            var employeeId = context.ActionArguments["id"] as Guid?;
            if (employeeId.HasValue)
            {
                {
                    var employee = _context.Employees.FirstOrDefault(s => s.Id == employeeId.Value);
                    if(employee == null)
                    {
                        context.ModelState.AddModelError("EmployeeId", "EmployeeId doesn't exits.");
                        var problemDetail = new ValidationProblemDetails(context.ModelState)
                        {
                            Status = StatusCodes.Status404NotFound
                        };
                        context.Result = new NotFoundObjectResult(problemDetail);
                    }
                }
            }
            else
            {
                    context.ModelState.AddModelError("EmployeeId", "EmployeeId is invalid");
                    var problemDetail = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "detail"
                    };
                    context.Result = new BadRequestObjectResult(problemDetail);
            }
        }
    }
}
