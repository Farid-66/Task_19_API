namespace API.Models.DTOs
{
    public class DtoStudent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int GroupId { get; set; }
        public DtoGroup Group { get; set; }
        public int ProfessionId { get; set; }
        public DtoProfession Profession { get; set; }
    }
}
