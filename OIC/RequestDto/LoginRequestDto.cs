namespace OIC.RequestDto
{
    public record LoginRequestDto
    {
        public string Username { get; set; }
        public string Password { get; set; }    
    }
}
