namespace BulletinBoard
{
    public class AnnouncementToUpdateDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public Guid SubcategoryId { get; set; }
    }
}
