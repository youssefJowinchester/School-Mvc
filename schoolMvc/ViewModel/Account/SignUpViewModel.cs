using System.ComponentModel.DataAnnotations;

namespace schoolMvc.PL.ViewModel.Account
{
    public class SignUpViewModel
    {

        [Required(ErrorMessage = "Email is Required !!")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "UserName is Required !!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Your Age is Required !!")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Phone is Required !!")]
        public string Phone { get; set; }
        public string Address { get; set; }

        [Required(ErrorMessage = "City is Required !!")]
        public string City { get; set; }

        [Required(ErrorMessage = "Password is Required !!")]
        [MinLength(5, ErrorMessage = "Minimum Password Lenght Is 5")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "ConfirmPassword is Required !!")]
        [Compare(nameof(Password), ErrorMessage = "Confirm Password does Not Match Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
