namespace DAL.Model
{
    public record Guardian:BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string  Relationship { get; set; }
        public string Email { get; set; }
        public string CellNumber { get; set; }
        public Student Student { get; set; }
    }
}
