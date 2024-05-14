using AutoMapper;
using DomainLayer.AcessLayer;
using DomainLayer.Interface.IService;
using LeaveManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.VIewModels;

namespace LeaveManagement.Controllers
{
    [Authorize]

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
       
        [HttpPost]
        public IActionResult CreateEmployee(EmployeeViewModel employee)
        {
            var employe = _service.CreateEmployee(employee);
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
