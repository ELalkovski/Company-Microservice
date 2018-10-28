namespace CompanyMicroservice.Tests
{
    using System.Linq;
    using System.Threading.Tasks;
    using CompanyMicroservice.Data;
    using CompanyMicroservice.Models;
    using CompanyMicroservice.Services;
    using CompanyMicroservice.Tests.Mocking;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CompanyServiceTests
    {
        private CompanyMsContext db;

        [TestMethod]
        public async Task AddCompany_Test()
        {
            // Arrange
            var companyService = new CompanyService(this.db);

            // Act 
            await companyService.Add(new Company
            {
                Id = 1,
                Name = "Test Company",
                Information = "Test Info."
            });

            var companies = this.db.Companies;

            // Assert
            Assert.AreEqual(1, companies.Count());
        }

        [TestMethod]
        public async Task DeleteCompany_Test()
        {
            // Arrange
            var companyService = new CompanyService(this.db);

            this.db.Companies.Add(new Company
            {
                Id = 2,
                Name = "Delete Company",
                Information = "Delete Info."
            });
            await this.db.SaveChangesAsync();

            var company = await this.db
                .Companies
                .FirstOrDefaultAsync(c => c.Name == "Delete Company");

            // Act 
            await companyService
                .Delete(company);

            // Assert
            Assert.AreEqual(true, company.IsDeleted);
        }

        [TestMethod]
        public async Task EditCompany_Test()
        {
            // Arrange
            var companyService = new CompanyService(this.db);

            this.db.Companies.Add(new Company
            {
                Id = 3,
                Name = "Edit Company",
                Information = "Edit Info."
            });
            await this.db.SaveChangesAsync();

            var company = await this.db
                .Companies
                .FirstOrDefaultAsync(c => c.Name == "Edit Company");

            company.Name = "Changed Name";
            company.Information = "Changed Info";

            // Act 
            await companyService.Edit(company);

            var companies = this.db.Companies;

            // Assert
            Assert.AreEqual(true, companies.Any(c => c.Name == "Changed Name" && c.Information == "Changed Info"));
        }

        [TestMethod]
        public async Task GetAllCompanies_Test()
        {
            // Arrange
            var companyService = new CompanyService(this.db);

            this.db.Companies
                .Add(new Company
                {
                    Id = 4,
                    Name = "First Company",
                    Information = "First Info."
                });

            this.db.Companies
                .Add(new Company
                {
                    Id = 5,
                    Name = "Second Company",
                    Information = "FiSecondrst Info."
                });

            await this.db.SaveChangesAsync();


            // Act 
            var companies = await companyService
                .GetAll();

            // Assert
            Assert.AreEqual(2, companies.Count);
        }

        [TestMethod]
        public async Task GetCompany_Test()
        {
            // Arrange
            var companyService = new CompanyService(this.db);

            this.db.Companies.Add(new Company
            {
                Id = 6,
                Name = "Get Company",
                Information = "Get Info."
            });
            await this.db.SaveChangesAsync();

            // Act 
            var company = await companyService
                .GetById(6);

            // Assert
            Assert.AreNotEqual(null, company);
        }

        [TestInitialize]
        public void InitializeDbContext()
        {
            this.db = MockDbContext.GetContext();
        }
    }
}
