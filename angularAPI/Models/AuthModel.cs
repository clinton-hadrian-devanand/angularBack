namespace angularAPI.Models
{
    public class AuthModel
    {
        public int? userId { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string? token { get; set; }
    }
}
