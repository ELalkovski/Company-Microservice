namespace CompanyMicroservice.App.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using CompanyMicroservice.App.Helpers;
    using CompanyMicroservice.Common.ViewModels;
    using CompanyMicroservice.Models;
    using CompanyMicroservice.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;

    public class EmployeesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IEmployeeService employeeService;

        public EmployeesController(IMapper mapper, IEmployeeService employeeService)
        {
            this.mapper = mapper;
            this.employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Add(int companyId)
        {
            var availableLevelsVm = await this.GetAvailableLevels();

            var employeeVm = new EmployeeVm
            {
                CompanyId = companyId,
                AvailableLevels = availableLevelsVm,
                StartingDate = DateTime.UtcNow,
            };

            return this.View(employeeVm);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EmployeeVm model)
        {
            if (!this.ModelState.IsValid)
            {
                model.AvailableLevels = await this.GetAvailableLevels();
                return this.View(model);
            }

            var employee = this.mapper.Map<Employee>(model);
            await this.employeeService.Add(employee);

            this.TempData["SuccessMsg"] = string.Format(SuccessMessage.Add, "Employee");

            return this.RedirectToAction("Details", "Companies", new {companyId = model.CompanyId});
        }

        [HttpGet]
        public async Task<IActionResult> Details(int employeeId)
        {
            var employee = await this.employeeService
                .GetById(employeeId);

            if (employee == null)
            {
                return this.RedirectToAction("Index", "Companies");
            }

            var employeeVm = this.mapper.Map<EmployeeVm>(employee);

            return this.View(employeeVm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int employeeId)
        {
            var employee = await this.employeeService
                .GetById(employeeId);

            if (employee == null)
            {
                return this.RedirectToAction("Index", "Companies");
            }

            var employeeVm = this.mapper.Map<EmployeeVm>(employee);
            employeeVm.AvailableLevels = await this.GetAvailableLevels();

            return this.View(employeeVm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EmployeeVm model)
        {
            if (!this.ModelState.IsValid)
            {
                model.AvailableLevels = await this.GetAvailableLevels();
                return this.View(model);
            }

            var employee = this.mapper.Map<Employee>(model);
            await this.employeeService.Edit(employee);

            this.TempData["SuccessMsg"] = string.Format(SuccessMessage.Edit, "Employee");

            return this.RedirectToAction("Details", "Employees", new { employeeId = model.Id });
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int employeeId)
        {
            var employee = await this.employeeService
                .GetById(employeeId);

            if (employee == null)
            {
                return this.RedirectToAction("Index", "Companies");
            }

            var companyId = employee.CompanyId;
            await this.employeeService.Delete(employee);

            this.TempData["SuccessMsg"] = string.Format(SuccessMessage.Delete, "Employee");

            return this.RedirectToAction("Details", "Companies", new {companyId});
        }

        private async Task<ICollection<ExperienceLevelVm>> GetAvailableLevels()
        {
            var availableLevels = await this.employeeService
                .GetExperienceLevels();
            var availableLevelsVm = this.mapper.Map<ICollection<ExperienceLevelVm>>(availableLevels);

            return availableLevelsVm;
        }
    }
}