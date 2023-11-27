namespace PersonalCollectionManagement.Business.DTOs.ItemDtos
{
    public class LastAddedItemForWiewDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string CollectionName { get; set; } = null!;
        public int CollectionId { get; set; }
        public string Author { get; set; } = null!;
    }
}
