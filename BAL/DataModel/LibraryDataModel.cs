namespace BAL.DataModel
{
    public record LibraryDataModel : BaseDataModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string LibrarianName { get; set; }
        public ICollection<StudentDataModel> Students {get;set;}
    }
}
