namespace Linkdev.Talabat.Dashboard.Models.Auth
{
	public class RoleViewModel
	{
        public string? Id { get; set; }

        public required string Name { get; set; }

        public bool IsSelected { get; set; }
    }
}
