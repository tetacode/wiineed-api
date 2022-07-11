namespace Data.Entity.Collection
{
    
    public partial class User : MongoDbEntity
    {
        public User()
        {
            Settings = new UserSettings();
        }

        public string Email { get; set; }

        public string Lastname { get; set; }

        public string Name { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public string Username { get; set; }
        
        public Guid BusinessId { get; set; }
        
        public UserSettings Settings { get; set; }
    }
}
