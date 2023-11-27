namespace PersonalCollectionManagement.Business.DTOs.CollectionDtos
{
    public class CollectionForUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Topic { get; set; } = null!;
        public List<CollectionFieldForUpdateDto> Fields { get; set; }
    }

    public class CollectionFieldForUpdateDto : CollectionFieldDto
    {
        public int Id { get; set; }
    }
}
