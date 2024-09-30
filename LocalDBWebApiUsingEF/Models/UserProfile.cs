namespace DataTierWebServer.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string? FName { get; set; }
        public string? LName { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public uint Age { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public string? Password { get; set; }
    }
}
