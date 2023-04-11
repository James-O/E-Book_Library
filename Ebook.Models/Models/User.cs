
using Microsoft.AspNetCore.Identity;

namespace E_Book_Library.Models
{
    public class User : IdentityUser
    {
        public int PublicId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime DateCreated { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Rating> Ratings { get; set; }
    }
}
