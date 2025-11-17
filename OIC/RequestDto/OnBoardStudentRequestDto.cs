namespace OIC.RequestDto
{
    public class OnBoardStudentRequestDto
    {
        public CreateStudentRequestDto student { get; set; }
        public CreateGuardianRequestDto guardian { get; set; }
        public List<CreateEducationRequestDto> educations { get; set; }
        public List<CreateLibraryRequestDto> libraries { get; set; }
        public CreateHostelRequestDto hostel { get; set; }
    }
}
