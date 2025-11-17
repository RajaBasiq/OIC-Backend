namespace OIC.RequestDto
{
    public record UpdateLibraryRequestDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string LibrarianName { get; set; }

    }
}
