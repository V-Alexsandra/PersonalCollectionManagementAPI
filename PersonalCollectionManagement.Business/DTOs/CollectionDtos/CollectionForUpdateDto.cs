namespace PersonalCollectionManagement.Business.DTOs.CollectionDtos
{
    public class CollectionForUpdateDto : CollectionForManipulationDto
    {
        public int Id { get; set; }
        public List<CollectionFieldForUpdateDto> Fields { get; set; }
    }

    public class CollectionFieldForUpdateDto : CollectionFieldDto
    {
        public int Id { get; set; }
    }
}
