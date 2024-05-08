using AutoMapper;
using DomainLayer.ViewModels;
using LeaveManagement.Models;
using PresentationLayer.ViewModels;
using PresentationLayer.VIewModels;

namespace LeaveManagement.AutoMapperProfile
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();

            CreateMap<Calander, CalanderViewModel>().ReverseMap();

            CreateMap<LeaveType, LeaveTypeViewModel>().ReverseMap();

            CreateMap<LeaveApply, LeaveApplyViewModel>().ReverseMap();

            CreateMap<User, LoginViewModel>().ReverseMap();

            CreateMap<User, RegisterViewModel>().ReverseMap();

        }


    }
}
