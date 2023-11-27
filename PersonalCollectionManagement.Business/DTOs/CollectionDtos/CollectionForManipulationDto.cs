namespace PersonalCollectionManagement.Business.DTOs.CollectionDtos
{
    public class CollectionForManipulationDto
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? ImageLink { get; set; }
        public string Topic { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public List<CollectionFieldDto> Fields { get; set; }
    }

    public class CollectionFieldDto
    {
        public string Type { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
