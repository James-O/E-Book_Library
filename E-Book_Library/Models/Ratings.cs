namespace E_Book_Library.Models
{
    public class Ratings
    {
        public int Id { get; set; }
        //public int RatingsId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        //public Ratings Ratings { get; set; }
        public DateTime DateCreated { get; set; }
        public Book Book { get; set; }
        public User User { get; set; }
    }
}
