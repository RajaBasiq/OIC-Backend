using System.ComponentModel.DataAnnotations;

namespace BAL.Dto
{
    public record StudentDto : BaseDto
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String Email { get; set; }
        public String BloodGroup { get; set; }
        public String CellNumber { get; set; }
        public long GuardianId { get; set; }
        public GuardianDto Guardian { get; set; }
        public HostelDto Hostel { get; set; }
        public ICollection<LibraryDto> Libraries { get; set; }
        public ICollection<EducationDto> Educations { get; set; }
    }
}
