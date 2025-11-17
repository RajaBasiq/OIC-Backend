namespace OIC.ResponseDto
{
    public record HostelResponseDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string WardenName { get; set; }
        public int Capacity { get; set; }

    }
}
