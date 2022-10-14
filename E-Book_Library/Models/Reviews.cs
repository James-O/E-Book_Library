namespace E_Book_Library.Models
{
    public class Reviews
    {
        public int Id { get; set; }
        //public int ReviewsId { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public string Comment { get; set; }
        public DateTime DateCreated { get; set; }
        public Book Books { get; set; }
        public User Users { get; set; }
    }
}
