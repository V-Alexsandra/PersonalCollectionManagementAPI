using PersonalCollectionManagement.Data.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalCollectionManagement.Business.DTOs.ItemDtos
{
    public class ItemForManipulationDto
    {
        public string Name { get; set; } = null!;
        public string? Tag { get; set; }
        public string? ImageLink { get; set; }
        public int CollectionId { get; set; }
    }
}
