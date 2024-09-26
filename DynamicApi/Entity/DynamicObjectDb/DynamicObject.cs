using System.ComponentModel.DataAnnotations;

namespace DynamicAPI.Entity.DynamicObjectDb
{
    public class Objects
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; }
        public string ObjectData { get; set; } 
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
