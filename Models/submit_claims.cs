using System.ComponentModel.DataAnnotations;

namespace part1_poe.Models
{
    public class submit_claims
    {
        //requiring all the fields on the claims form
        [Required(ErrorMessage = "Number of sessions is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Sessions must be at least 1")]
        public int Sessions { get; set; }

        [Required(ErrorMessage = "Number of hours is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Hours must be at least 1")]
        public int Hours { get; set; }

        [Required(ErrorMessage = "Rate amount is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Rate must be positive")]
        public decimal Rate { get; set; }

        [Display(Name = "Total Amount")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Module name is required")]
        public string Module { get; set; }

        [Required(ErrorMessage = "Faculty name is required")]
        public string Faculty { get; set; }

        [Display(Name = "Supporting Documents")]
        public IFormFile? Documents { get; set; }
    }
}
