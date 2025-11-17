namespace DAL.Model
{
    public record Education:BaseEntity
    {
        public string DegreeName { get; set; }
        public int StartingYear { get; set; }
        public int? EndingYear { get; set; }
        public string Institute { get; set; }
        public double ResultPercentage { get; set; }
        public bool Ongoing { get; set; }
        public long StudentId { get; set; }
        public Student Student { get; set; }
    }
}
