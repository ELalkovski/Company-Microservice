namespace CompanyMicroservice.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CompanyMicroservice.Models;

    public interface ICompanyService
    {
        Task Add(Company company);

        Task Edit(Company company);

        Task Delete(Company company);

        Task<bool> CompanyExists(string name);

        Task<Company> GetById(int id);

        Task<ICollection<Company>> GetAll();
    }
}
