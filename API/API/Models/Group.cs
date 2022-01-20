using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(10)]
        public string Name { get; set; }
    }
}
