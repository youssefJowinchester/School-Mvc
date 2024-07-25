using System.ComponentModel.DataAnnotations;

namespace schoolMvc.PL.ViewModel.Account
{
    public class SignInViewModel
    {



        [Required(ErrorMessage = "Email is Required !!")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Password is Required !!")]
        [MinLength(5, ErrorMessage = "Minimum Password Lenght Is 5")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

    }
}
