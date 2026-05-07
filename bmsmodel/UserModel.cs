using System.ComponentModel.DataAnnotations;

namespace bmsmodel.Common
{
    public class UserModel : BaseModel
    {
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }

        [Required]
        public string Mobile { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
