using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebIdentity.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

    }
    public class RegisterEditViewModel
    {
        public string Email { get; set; }

        public string ID { get; set; }


    }
    public class RegisterListViewModel
    {

        public List<IdentityUser> Users;
    }
}
