namespace CompanyMicroservice.App.Mapping
{
    using AutoMapper;
    using CompanyMicroservice.Common.ViewModels;
    using CompanyMicroservice.Models;

    public class ExperienceLevelProfile : Profile
    {
        public ExperienceLevelProfile()
        {
            this.CreateMap<ExperienceLevel, ExperienceLevelVm>()
                .ReverseMap();
        }
    }
}
