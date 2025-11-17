namespace DAL.Dto
{
    public record OnBoardStudentDto
    {
        public StudentDto student { get; set; }
        public GuardianDto guardian { get; set; }
        public List<EducationDto> educations { get; set; }
        public List<LibraryDto> libraries { get; set; }
        public HostelDto? hostel { get; set; }
    }
}
