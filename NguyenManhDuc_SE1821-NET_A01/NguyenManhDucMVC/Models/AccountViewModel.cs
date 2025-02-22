using System.ComponentModel.DataAnnotations;

namespace NguyenManhDucMVC.Models
{
    public class AccountViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
