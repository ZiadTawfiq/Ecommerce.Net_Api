namespace WebApplication1.Security
{
    public class JwtTokenOptions
    {
        public string issuer { get; set; }
        public string audience { get; set; }
        public string key { get; set; }
        public int lifetime { get; set; }
    }

}
