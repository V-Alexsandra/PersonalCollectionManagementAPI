namespace PersonalCollectionManagement.Business.DTOs.ItemDtos
{
    public class ItemForManipulationDto
    {
        public string Name { get; set; } = null!;
        public List<TagDto> Tags { get; set; }
        public string? ImageLink { get; set; }
        public int CollectionId { get; set; }
        public List<ItemFieldValuesDto> ItemFieldValues { get; set; }
    }

    public class TagDto
    {
        public string? Tag { get; set; }
    }

    public class ItemFieldValuesDto
    {
        public string Value { get; set; }
        public int CollectionFieldId { get; set; }
    }
}
