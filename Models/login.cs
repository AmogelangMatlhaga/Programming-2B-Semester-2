using System.ComponentModel.DataAnnotations;

namespace part1_poe.Models
{
    public class login
    {
        //customised messages for the login error handling

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }


    }
}
