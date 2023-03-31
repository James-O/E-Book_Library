namespace E_Book_Library.Models
{
    public class Book
    {
        public int Id { get; set; }
        
        public int PublicId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; }
        public string Publisher { get; set; }
        public string Isbn { get; set; }
        public string AvatarUr { get; set; }
        public string Description { get; set; }
        public string Pages { get; set; }
        public string Author { get; set; }
        public string CopiesAvailable { get; set; }
        public DateTime DatePublished { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Rating> Ratings { get; set; }
        public Category Category { get; set; }
    }
}
