namespace CompanyMicroservice.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CompanyMicroservice.Data;
    using CompanyMicroservice.Models;
    using CompanyMicroservice.Services.Contracts;
    using Microsoft.EntityFrameworkCore;

    public class CompanyService : ICompanyService
    {
        private readonly CompanyMsContext db;

        public CompanyService(CompanyMsContext db)
        {
            this.db = db;
        }

        public async Task Add(Company company)
        {
            this.db
                .Companies
                .Add(company);

            await this.db.SaveChangesAsync();
        }

        public async Task Edit(Company company)
        {
            this.db
                .Companies
                .Update(company);

            await this.db.SaveChangesAsync();
        }

        public async Task Delete(Company company)
        {
            if (company.Employees != null && company.Employees.Any())
            {
                await this.DeleteAllEmployees(company.Employees);
            }

            company.IsDeleted = true;

            this.db
                .Companies
                .Update(company);

            await this.db.SaveChangesAsync();
        }

        public async Task<bool> CompanyExists(string name)
        {
            var company = await this.db
                .Companies
                .FirstOrDefaultAsync(c => c.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

            return company != null;
        }

        public async Task<Company> GetById(int id)
        {
            var company = await this.db
                .Companies
                .Include(c => c.Employees)
                .ThenInclude(e => e.ExperienceLevel)
                .FirstOrDefaultAsync(c => c.Id == id);

            company.Employees = company
                .Employees
                .Where(e => !e.IsDeleted)
                .ToList();

            return company;
        }

        public async Task<ICollection<Company>> GetAll()
        {
            var companies = await this.db
                .Companies
                .Where(c => !c.IsDeleted)
                .ToListAsync();

            return companies;
        }

        private async Task DeleteAllEmployees(ICollection<Employee> companyEmployees)
        {
            foreach (var employee in companyEmployees)
            {
                employee.IsDeleted = true;
                this.db.Update(employee);
            }

            await this.db.SaveChangesAsync();
        }
    }
}
