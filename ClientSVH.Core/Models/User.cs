namespace ClientSVH.Core.Models
{
    public class User 
    {
        private User(Guid id, string username, string passwordHash, string email)
        {
           
            Id=id;
            UserName = username;
            PasswordHash = passwordHash;
            Email = email;
        }

        public Guid Id { get; set; }
        public string UserName { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public static User Create(Guid id, string username, string passwordHash, string email)
        {
            var user = new User(id, username, passwordHash, email);
            return user;
        }
    }
}
