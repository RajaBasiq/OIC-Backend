namespace BAL.Dto
{
    public record LoginDto
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
