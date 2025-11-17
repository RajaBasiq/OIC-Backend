namespace DAL.Dto
{
    public record GuardianDto: BaseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string  Relationship { get; set; }
        public string Email { get; set; }
        public string CellNumber { get; set; }
        public StudentDto Student { get; set; }
    }
}
