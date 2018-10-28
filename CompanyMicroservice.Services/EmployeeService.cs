namespace CompanyMicroservice.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CompanyMicroservice.Data;
    using CompanyMicroservice.Models;
    using CompanyMicroservice.Services.Contracts;
    using Microsoft.EntityFrameworkCore;

    public class EmployeeService : IEmployeeService
    {
        private readonly CompanyMsContext db;

        public EmployeeService(CompanyMsContext db)
        {
            this.db = db;
        }

        public async Task Add(Employee employee)
        {
            this.db
                .Employees
                .Add(employee);

            await this.db.SaveChangesAsync();
        }

        public async Task Edit(Employee employee)
        {
            this.db
                .Employees
                .Update(employee);

            await this.db.SaveChangesAsync();
        }

        public async Task Delete(Employee employee)
        {
            employee.IsDeleted = true;

            this.db
                .Employees
                .Update(employee);

            await this.db.SaveChangesAsync();
        }

        public async Task<Employee> GetById(int id)
        {
            var employee = await this.db
                .Employees
                .Include(e => e.Company)
                .Include(e => e.ExperienceLevel)
                .FirstOrDefaultAsync(e => e.Id == id);

            return employee;
        }

        public async Task<ICollection<ExperienceLevel>> GetExperienceLevels()
        {
            var experienceLevels = await this.db
                .ExperienceLevels
                .ToListAsync();

            return experienceLevels;
        }
    }
}
