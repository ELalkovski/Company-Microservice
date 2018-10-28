namespace CompanyMicroservice.App.Mapping
{
    using AutoMapper;
    using CompanyMicroservice.Common.ViewModels;
    using CompanyMicroservice.Models;

    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            this.CreateMap<Company, CompanyVm>()
                .ReverseMap();
        }
    }
}
