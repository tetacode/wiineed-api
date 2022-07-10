using System;
using System.Collections.Generic;
using Core.Repository.Abstract;

namespace Data.Entity
{
    
    public partial class User : MongoDbEntity
    {
        public User()
        {
            
        }

        public string Email { get; set; }

        public string Lastname { get; set; }

        public string Name { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public string Username { get; set; }
    }
}
