namespace TribuneWatch.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int CityId { get; set; }
        public int RoleId { get; set; }
        public bool UserPriority { get; set; }
        public string? OffDay { get; set; }
        public bool IsActive { get; set; }
    }
}
