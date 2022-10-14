namespace E_Book_Library.Models
{
    public class User
    {
        public int PublicId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string AvatarUrl { get; set; }
        public DateTime DateCreated { get; set; }
        public string Reviews { get; set; }
        public decimal Ratings { get; set; }
    }
}
