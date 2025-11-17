namespace DAL.Model
{
    public record Hostel:BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string WardenName { get; set; }
        public int Capacity { get; set; }
        public ICollection<Student> Students { get; set; }

    }
}
