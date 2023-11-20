using PersonalCollectionManagement.Data.Entities;

namespace PersonalCollectionManagement.Business.DTOs.CommentDtos
{
    public class CommentForManipulationDto
    {
        public string Text { get; set; } = null!;
        public DateTime Date { get; set; }
        public int ItemId { get; set; }
        public string UserId { get; set; }
    }
}
