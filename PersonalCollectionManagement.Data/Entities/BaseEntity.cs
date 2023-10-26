using System.ComponentModel.DataAnnotations;

namespace PersonalCollectionManagement.Data.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
