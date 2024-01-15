using NotefyMe.Domain.Validations;
using System.ComponentModel.DataAnnotations;

namespace NotefyMe.WebApp.ViewModels
{
    public class RegisterViewModel
    {
        [StringLength(120, ErrorMessage = "Name can't be more than 120 characters")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Date of birth")]
        [MinimumAge(6)]
        [MaximumAge(100)]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }

        [StringLength(25, ErrorMessage = "Username can't be more than 25 characters")]
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
