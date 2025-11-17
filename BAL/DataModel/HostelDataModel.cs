namespace BAL.DataModel
{
    public record HostelDataModel: BaseDataModel
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string WardenName { get; set; }
        public int Capacity { get; set; }
        public ICollection<StudentDataModel> Students { get; set; }

    }
}
