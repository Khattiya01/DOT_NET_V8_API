using Microsoft.AspNetCore.Mvc.Filters;
using WebAppAPI.Models.Repositorys;
using WebAppAPI.Models;
using Microsoft.AspNetCore.Mvc;
using WebAppAPI.Data;

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
                var existingEmployee = ShirtRepository.GetShirtByProperties(shirt.Brand, shirt.Gender, shirt.Color, shirt.Size);

                if (existingShirt != null)
                {
                    context.ModelState.AddModelError("Shirt", "Shirt already exists...");
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
