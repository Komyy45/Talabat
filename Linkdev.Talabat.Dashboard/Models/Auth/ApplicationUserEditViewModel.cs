using System.ComponentModel.DataAnnotations;

namespace Linkdev.Talabat.Dashboard.Models.Auth
{
	public class ApplicationUserEditViewModel
	{
        public string? Id { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public List<RoleViewModel> Roles { get; set; } = null!;
    }
}
