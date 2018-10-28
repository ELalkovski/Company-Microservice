namespace CompanyMicroservice.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Company
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Information { get; set; }

        public string CompanyLogo { get; set; }

        public bool IsDeleted { get; set; }                

        public ICollection<Employee> Employees { get; set; }
    }
}
