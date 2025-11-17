namespace OIC.RequestDto
{
    public record UpdateStudentRequestDto
    {
        public long Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String Email { get; set; }
        public String BloodGroup { get; set; }
        public String CellNumber { get; set; }
        public CreateGuardianRequestDto guardian { get; set; }
        public List<CreateEducationRequestDto> educations { get; set; }
        public List<CreateLibraryRequestDto> libraries { get; set; }
        public CreateHostelRequestDto hostel { get; set; }
    }
}
