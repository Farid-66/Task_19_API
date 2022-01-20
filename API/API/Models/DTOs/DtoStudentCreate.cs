using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs
{
    public class DtoStudentCreate
    {
        [MaxLength(150)]
        public string Name { get; set; }
        public string Surname { get; set; }
        public int GroupId { get; set; }
        public int ProfessionId { get; set; }
    }
}
