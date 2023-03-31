namespace E_Book_Library.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int RatingCount { get; set; }
        public DateTime DateCreated { get; set; }
        public Book Book { get; set; }
        public User User { get; set; }
    }
}
