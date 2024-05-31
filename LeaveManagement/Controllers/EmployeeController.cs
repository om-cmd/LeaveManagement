using DomainLayer.Interface.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.VIewModels;
using System.Threading.Tasks;

namespace LeaveManagement.Controllers
{
    /// <summary>
    /// provides the methods to manage employe and their details
    /// </summary>
    [Authorize(Roles = "SuperAdmin,Admin")]
    [ApiController]
    [Route("api/[controller]")]
    /// <summary>
    ///Initializes a new instance of the <see cref="EmployeeController"/> class.
    /// </summary>
    /// <param name="unitOfWork">used for database acess operations</param>
    /// <param name="mapper">the mapper of object and view model mapping using automapper</param>
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;

        public EmployeeController(IEmployeeService employeeService)
        {
            _service = employeeService;
        }

        /// <summary>
        /// it is the method to get the list of employee
        /// </summary>
        /// <returns>returns employee by searching form the employee table which status are active or not deleted </returns>

        [HttpGet]
        [ProducesResponseType(200)]
        public IActionResult EmployeeList()
        {
            var employee = _service.EmployeeList();
            return Ok(employee);
        }

        /// <summary>
        /// used to update the existing employee
        /// </summary>
        /// <param name="model">The employee view model containing updated information </param>
        /// <param name="id">The identifier of the employee to update</param>
        /// <returns>The updated employee entity, or null if the employee was not found.</returns>

        [HttpPut("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public IActionResult UpdateEmployee(EmployeeViewModel employee, int id)
        {
            var updatedEmployee = _service.UpdateEmployee(employee, id);
            if (updatedEmployee == null)
            {
                return NotFound();
            }
            return Ok(updatedEmployee);
        }
        /// <summary>
        /// this is used for deleting the emoloyee
        /// </summary>
        /// <param name="id">it takes id as paramter to delete the id of employee</param>
        /// <returns></returns>

        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult DeleteEmployee(int id)
        {
            var deletedEmployee = _service.DeleteEmployee(id);
            if (deletedEmployee == null)
            {
                return NotFound();
            }
            return Ok(deletedEmployee);
        }
    }
}
