namespace BAL.DataModel
{
    public record StudentDataModel : BaseDataModel
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String Email { get; set; }
        public String BloodGroup { get; set; }
        public String CellNumber { get; set; }
        public long GuardianId { get; set; }
        public GuardianDataModel Guardian { get; set; }
        public HostelDataModel Hostel { get; set; }
        public ICollection<LibraryDataModel> Libraries { get; set; }
        public ICollection<EducationDataModel> Educations { get; set; }

    }
}
