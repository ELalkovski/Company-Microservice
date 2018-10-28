namespace CompanyMicroservice.Common.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class EmployeeVm
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        [DisplayName("Full Name")]
        public string FullName { get; set; }

        [Required]
        [RegularExpression(@"^\d{1,18}.\d{2}$", ErrorMessage = "Salary must have two decimal places")]
        [Range(0.0, Double.PositiveInfinity)]
        public decimal? Salary { get; set; }

        [Required]
        [DisplayName("Vacation Days")]
        [Range(0, 30)]
        public int? VacationDays { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Starting Date")]
        public DateTime StartingDate { get; set; }

        [Required]
        public int CompanyId { get; set; }
        public CompanyVm Company { get; set; }

        [Required]
        [DisplayName("Experience Level")]
        public int ExperienceLevelId { get; set; }
        public ExperienceLevelVm ExperienceLevel { get; set; }

        public ICollection<ExperienceLevelVm> AvailableLevels { get; set; }
    }
}
