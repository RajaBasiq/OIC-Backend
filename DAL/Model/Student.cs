namespace DAL.Model
{
    public record Student : BaseEntity
    {
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public String Email { get; set; }
        public String BloodGroup { get; set; }
        public String CellNumber { get; set; }
        public long GuardianId { get; set; }
        public Guardian Guardian { get; set; }
        public long? HostelId { get; set; }
        public Hostel Hostel { get; set; }
        public ICollection<Library> Libraries { get; set; }
        public ICollection<Education> Educations { get; set; }

    }
}
