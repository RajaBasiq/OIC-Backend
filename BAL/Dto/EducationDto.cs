namespace BAL.Dto
{
    public record EducationDto: BaseDto
    {
        public string DegreeName { get; set; }
        public int StartingYear { get; set; }
        public int? EndingYear { get; set; }
        public string Institute { get; set; }
        public double ResultPercentage { get; set; }
        public bool Ongoing { get; set; }
        public StudentDto Student { get; set; }
    }
}
