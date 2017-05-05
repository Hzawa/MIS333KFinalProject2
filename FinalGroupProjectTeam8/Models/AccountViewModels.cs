using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FinalGroupProjectTeam8.Models
{

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FName { get; set; }

        [Required(ErrorMessage = "Please enter your Phone Number")]
        //[RegularExpression("(/d/d/d)/d/d/d-/d/d/d/d",
        //ErrorMessage = "Please enter a valid phone number number")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Middle Initial")]
        public String MiddleInitial { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last name is required.")]
        public String LName { get; set; }

        [Display(Name = "Birthday (month/day/year)")]
        [Required(ErrorMessage = "Birthday is required.")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Street Address")]
        [Required(ErrorMessage = "Street is required.")]
        public String Street { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City is required.")]
        public String City { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "State is required.")]
        public String State { get; set; }

        [Display(Name = "Zipcode")]
        [Required(ErrorMessage = "Zipcode is required.")]
        public String Zip { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Birth Year")]
        public string BirthYear { get; set; }
    }
}
