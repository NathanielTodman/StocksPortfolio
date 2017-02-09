using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksPortfolio.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Username is required"), MaxLength(256)]
        public string Username { get; set; }

        [Required(ErrorMessage = "E-mail address is required")]
        [EmailAddress]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Compare(nameof(Password), ErrorMessage = "Passwords don't match")]
        [DataType(DataType.Password)]
        public string Password2 { get; set; }

    }
}
