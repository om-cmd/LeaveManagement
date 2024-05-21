using DomainLayer.Interface.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.VIewModels;

namespace LeaveManagement.Controllers
{
    [Authorize(Roles = "SuperAdmin,Admin")]


    [ApiController]
    [Route("api/[controller]")]

    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service ;

        public EmployeeController(IEmployeeService employeeService)
        {
            _service = employeeService;
           
        }
        [HttpGet]
        public async Task<IActionResult> EmployeeList()
        {
           var employee = _service.EmployeeList();
            return Ok(employee);
        }
       
       
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(EmployeeViewModel employee, int id)
        {
           var employes = _service.UpdateEmployee(employee, id);
            return Ok(employee);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var delete = _service.DeleteEmployee(id);
            return Ok(delete);
        }


    }
}
