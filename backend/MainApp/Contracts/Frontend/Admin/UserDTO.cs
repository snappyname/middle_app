namespace Contracts.Frontend.Admin
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set; }
        public List<Guid> AssignedSensors { get; set; } = [];
    }
}
