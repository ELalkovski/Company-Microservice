namespace CompanyMicroservice.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ExperienceLevel
    {
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
