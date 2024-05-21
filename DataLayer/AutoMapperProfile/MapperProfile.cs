using AutoMapper;
using DomainLayer.Models;
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
            CreateMap<EmployeeViewModel, Employee>()
          .ForMember(x => x.JoinedDate, y => y.MapFrom(src => DateTime.Now));

            CreateMap<Employee, PersonBaseModel>().ReverseMap();
            CreateMap<User, PersonBaseModel>().ReverseMap();


            CreateMap<Calander, CalanderViewModel>().ReverseMap();

            CreateMap<LeaveType, LeaveTypeViewModel>().ReverseMap();

            CreateMap<LeaveApply, LeaveApplyViewModel>().ReverseMap();

            CreateMap<User, LoginViewModel>().ReverseMap();

            CreateMap<User, RegisterViewModel>()
                 .ForMember(dest => dest.Password, opt => opt.Ignore());
            CreateMap<RegisterViewModel, User>()
                 .ForMember(dest => dest.Password, opt => opt.Ignore());
             
            CreateMap<Person, PersonBaseModel>().ReverseMap()
                 .ForMember(dest => dest.Password, opt => opt.Ignore());
            ;



        }


    }
}
