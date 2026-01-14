using Microsoft.EntityFrameworkCore;

namespace TribuneAPI.Models
{
    [Keyless]
    public class User
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
