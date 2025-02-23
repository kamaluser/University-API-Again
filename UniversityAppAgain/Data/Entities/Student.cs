namespace UniversityAppAgain.Data.Entities
{
    public class Student:AuditEntity
    {
        public string FullName { get; set; }
        public string Email { get; set; }   
        public DateTime BirthDate { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
