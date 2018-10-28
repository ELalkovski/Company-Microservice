namespace CompanyMicroservice.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CompanyMicroservice.Models;

    public interface IEmployeeService
    {
        Task Add(Employee employee);

        Task Edit(Employee employee);

        Task Delete(Employee employee);

        Task<Employee> GetById(int id);

        Task<ICollection<ExperienceLevel>> GetExperienceLevels();
    }
}
