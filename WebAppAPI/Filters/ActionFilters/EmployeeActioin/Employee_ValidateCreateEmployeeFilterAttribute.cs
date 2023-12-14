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
        private readonly DataContext _context;

        public Employee_ValidateCreateEmployeeFilterAttribute(DataContext context)
        {
            _context = context;
        }

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
            else
            {
                var existingEmployee = _context.Employees.FirstOrDefault(x => x.Phone == employee.Phone);

                if (existingEmployee != null)
                {
                    context.ModelState.AddModelError("Employee", "Employee already exists...");
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Status = StatusCodes.Status400BadRequest
                    };
                    context.Result = new BadRequestObjectResult(problemDetails);
                }
            }

        }
    }
}
