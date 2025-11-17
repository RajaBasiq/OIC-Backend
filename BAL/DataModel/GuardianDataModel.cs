namespace BAL.DataModel
{
    public record GuardianDataModel: BaseDataModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string  Relationship { get; set; }
        public string Email { get; set; }
        public string CellNumber { get; set; }
        public StudentDataModel Student { get; set; }
    }
}
