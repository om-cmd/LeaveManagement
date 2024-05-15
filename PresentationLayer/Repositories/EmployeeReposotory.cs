using AutoMapper;
using DomainLayer.AcessLayer;
using DomainLayer.IRepoInterface.IRepo;
using LeaveManagement.Models;
using PresentationLayer.VIewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Repositories
{
    public class EmployeeReposotory : IEmployeeRepo
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeReposotory(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Employee CreateEmployee(EmployeeViewModel model)
        {
            var employee = _mapper.Map<Employee>(model);
            employee.JoinedDate = DateTime.Now;
            _unitOfWork.Context.Employee.Add(employee);
            _unitOfWork.Context.SaveChanges();
            return employee;
        }

        public EmployeeViewModel DeleteEmployee(int id)
        {
            var existingEmployee = _unitOfWork.Context.Employee.FirstOrDefault(x => x.Id == id);
            if (existingEmployee == null)
            {
                return null;
            }
            existingEmployee.Status = DomainLayer.Models.Status.Active;
            _unitOfWork.Context.SaveChanges();
            return _mapper.Map<EmployeeViewModel>(existingEmployee);
        }

        public ICollection<EmployeeViewModel> EmployeeList()
        {
            var employees = _unitOfWork.Context.Employee.Where(emp => emp.Status != DomainLayer.Models.Status.InActive).ToList();
            return _mapper.Map<List<EmployeeViewModel>>(employees);
        }

        public EmployeeViewModel GetEmployee(int id)
        {
            var employee = _unitOfWork.Context.Employee.Find(id);
            return _mapper.Map<EmployeeViewModel>(employee);
        }

        public Employee UpdateEmployee(EmployeeViewModel model, int id)
        {
            var existingEmployee = _unitOfWork.Context.Employee.FirstOrDefault(x => x.Id == id);
            if (existingEmployee == null)
            {
                return null;
            }
            _mapper.Map(model, existingEmployee);
            _unitOfWork.Context.SaveChanges();
            return existingEmployee;
        }
    }
}
