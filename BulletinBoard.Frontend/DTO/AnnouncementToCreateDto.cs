namespace BulletinBoard
{
    public class AnnouncementToCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Status { get; set; }
        public Guid SubcategoryId { get; set; }
    }
}
