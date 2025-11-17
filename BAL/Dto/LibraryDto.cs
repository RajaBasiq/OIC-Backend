namespace BAL.Dto
{
    public record LibraryDto : BaseDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string LibrarianName { get; set; }
        public ICollection<StudentDto> Students {get;set;}
    }
}
