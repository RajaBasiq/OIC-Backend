using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Dto
{
    public record UpdateStudentDto
    {
        public long Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String Email { get; set; }
        public String BloodGroup { get; set; }
        public String CellNumber { get; set; }
        public GuardianDto guardian { get; set; }
        public List<EducationDto> educations { get; set; }
        public List<LibraryDto> libraries { get; set; }
        public HostelDto hostel { get; set; }
    }
}
