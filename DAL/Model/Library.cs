namespace DAL.Model
{
    public record Library : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string LibrarianName { get; set; }
        public ICollection<Student> Students {get;set;}
    }
}
