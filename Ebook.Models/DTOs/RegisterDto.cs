using System.ComponentModel.DataAnnotations;

namespace E_Book_Library.DTOs
{
    public class RegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password must match")]
        public string ConfirmPassword { get; set; }
    }
}
