using System.Reflection.Metadata.Ecma335;

namespace OIC.ResponseDto
{
    public record StudentResponseDto
    {
        public long Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String Email { get; set; }
        public String BloodGroup { get; set; }
        public String CellNumber { get; set; }
        public GuardianResponseDto Guardian { get; set; }
        public HostelResponseDto Hostel { get; set; }
        public ICollection<LibraryResponseDto> Libraries { get; set; }
        public ICollection<EducationResponseDto> Educations { get; set; }
    }
}
