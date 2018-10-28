namespace CompanyMicroservice.App.Mapping
{
    using AutoMapper;
    using CompanyMicroservice.Common.ViewModels;
    using CompanyMicroservice.Models;

    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            this.CreateMap<Employee, EmployeeVm>()
                .ReverseMap();
        }
    }
}
