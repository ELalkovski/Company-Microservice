namespace CompanyMicroservice.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Employee
    {
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Salary { get; set; }

        [Required]
        public int VacationDays { get; set; }

        [Required]
        public DateTime StartingDate { get; set; }

        [Required]
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        [Required]
        public int ExperienceLevelId { get; set; }
        public ExperienceLevel ExperienceLevel { get; set; }
    }
}
