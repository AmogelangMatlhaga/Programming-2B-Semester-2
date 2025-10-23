using System.ComponentModel.DataAnnotations;

namespace part1_poe.Models
{
    public class register
    {
        //prompted all the fields on the registered page to be required

        [Required(ErrorMessage = "First name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Role is required")]
        public string Role { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; }
    }
}
