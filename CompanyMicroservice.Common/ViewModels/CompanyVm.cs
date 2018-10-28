namespace CompanyMicroservice.Common.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CompanyVm
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Information { get; set; }

        public string CompanyLogo { get; set; }

        public bool IsDeleted { get; set; }

        public ICollection<EmployeeVm> Employees { get; set; }
    }
}
