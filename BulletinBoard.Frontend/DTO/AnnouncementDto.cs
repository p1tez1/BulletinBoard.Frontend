namespace BulletinBoard
{
    public class AnnouncementDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } 
        public string Description { get; set; } 
        public DateTime CreatedAt { get; set; }
        public bool Status { get; set; }
        public Guid SubcategoryId { get; set; }
    }
}
