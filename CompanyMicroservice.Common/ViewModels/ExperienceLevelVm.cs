namespace CompanyMicroservice.Common.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class ExperienceLevelVm
    {
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }
    }
}
