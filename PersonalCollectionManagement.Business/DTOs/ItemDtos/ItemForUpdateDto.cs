namespace PersonalCollectionManagement.Business.DTOs.ItemDtos
{
    public class ItemForUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int CollectionId { get; set; }
        public List<ItemFieldValuesForUpdateDto> ItemFieldValues { get; set; }
    }

    public class TagForUpdateDto : TagDto
    {
        public int Id { set; get; }
        public int ItemId { set; get; }
    }

    public class ItemFieldValuesForUpdateDto : ItemFieldValuesDto
    {
        public int Id { set; get; }
    }
}
