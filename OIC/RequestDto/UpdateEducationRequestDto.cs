namespace OIC.RequestDto
{
    public class UpdateEducationRequestDto
    {
        public long Id { get; set; }
        public string DegreeName { get; set; }
        public int StartingYear { get; set; }
        public int? EndingYear { get; set; }
        public string Institute { get; set; }
        public double ResultPercentage { get; set; }
        public bool Ongoing { get; set; }
    }
}
