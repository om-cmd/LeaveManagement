using AutoMapper;
using DomainLayer.AcessLayer;
using LeaveManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PresentationLayer.VIewModels;

namespace LeaveManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class EmployeeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EmployeeController(IUnitOfWork dbContext, IMapper mapper)
        {
            _unitOfWork = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> EmployeeList()
        {
            try
            {

                IQueryable<Employee> modelQuery = _unitOfWork.Context.Employee.Where(emp=>emp.Status != DomainLayer.Models.Status.Active);
                IQueryable<EmployeeViewModel> vmQuery = _mapper.ProjectTo<EmployeeViewModel>(modelQuery);
                List<EmployeeViewModel> list = await vmQuery.ToListAsync();

                return Ok(list); // Assuming you want to return the list of employees
            }
            catch (Exception ex)
            {
                // Log the exception somewhere
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public IActionResult EmployeeID(int id)
        {
            try
            {
                var employee = _unitOfWork.Context.Employee.Find(id);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured:{ex.Message}");
            }

        }
        [HttpPost]
        public IActionResult CreateEmployee(EmployeeViewModel employee)
        {
            try
            {
                var create = _mapper.Map<Employee>(employee);
                create.JoinedDate = DateTime.Now;
                _unitOfWork.Context.Add(create);
                _unitOfWork.Context.SaveChanges();
                return Ok(create);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured:{ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(EmployeeViewModel employee, int id)
        {
            var existingEmployee = _unitOfWork.Context.Employee.FirstOrDefault(x => x.EmployeeID == id);
            if (existingEmployee == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "no employee found");
            }
            try
            {
                _mapper.Map(employee, existingEmployee);
                _unitOfWork.Context.SaveChanges();
                return Ok(existingEmployee);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occured:{ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var existingEmployee = _unitOfWork.Context.Employee.FirstOrDefault(x => x.EmployeeID == id);
            if (existingEmployee == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "no employee found");

            }
            try
            {
                existingEmployee.Status= DomainLayer.Models.Status.InActive;
                _unitOfWork.Context.SaveChanges();
                return Ok(existingEmployee);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An error occured:{ex.Message}");

            }
        }


    }
}
