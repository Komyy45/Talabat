namespace Linkdev.Talabat.Dashboard.Models.Auth
{
    public class ApplicationUserViewModel
    {
        public required string Id { get; set; }
        public required string UserName { get; set; }
        public required string DisplayName { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public IEnumerable<string> Roles { get; set; } = null!;
    }
}
