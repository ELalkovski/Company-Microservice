namespace CompanyMicroservice.App.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using CompanyMicroservice.App.Helpers;
    using CompanyMicroservice.Common.ViewModels;
    using CompanyMicroservice.Models;
    using CompanyMicroservice.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;

    public class CompaniesController : Controller
    {
        private readonly ICompanyService companyService;
        private readonly IMapper mapper;

        public CompaniesController(ICompanyService companyService, IMapper mapper)
        {
            this.companyService = companyService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var companies = await this.companyService
                .GetAll();

            var companiesVm = this.mapper.Map<ICollection<CompanyVm>>(companies)
                .OrderByDescending(c => c.Id)
                .ToList();


            return this.View(companiesVm);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int companyId)
        {
            var company = await this.companyService
                .GetById(companyId);

            if (company == null)
            {
                return this.RedirectToAction("Index");
            }

            var companyVm = this.mapper.Map<CompanyVm>(company);

            return this.View(companyVm);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var companyVm = new CompanyVm();

            return this.View(companyVm);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CompanyVm model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var companyExists = await this.companyService
                .CompanyExists(model.Name);

            if (companyExists)
            {
                this.ModelState.AddModelError("Name", "Company with that name already exists.");

                return this.View(model);
            }

            var company = this.mapper.Map<Company>(model);
            await this.companyService.Add(company);

            this.TempData["SuccessMsg"] = string.Format(SuccessMessage.Add, "Company");

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int companyId)
        {
            var company = await this.companyService
                .GetById(companyId);

            if (company == null)
            {
                return this.RedirectToAction("Index");
            }

            var companyVm = this.mapper.Map<CompanyVm>(company);

            return this.View(companyVm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CompanyVm model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            var company = this.mapper.Map<Company>(model);
            await this.companyService.Edit(company);

            this.TempData["SuccessMsg"] = string.Format(SuccessMessage.Edit, "Company");

            return this.RedirectToAction("Details", new {companyId = model.Id});
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int companyId)
        {
            var company = await this.companyService
                .GetById(companyId);

            if (company == null)
            {
                return this.RedirectToAction("Index");
            }

            await this.companyService
                .Delete(company);

            this.TempData["SuccessMsg"] = string.Format(SuccessMessage.Delete, "Company");

            return this.RedirectToAction("Index");
        }
    }
}