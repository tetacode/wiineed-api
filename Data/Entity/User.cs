﻿using System;
using System.Collections.Generic;
using Core.Repository.Abstract;

namespace Data.Entity
{
    public partial class User : IEntity
    {
        public User()
        {
            Roles = new HashSet<Role>();
        }

        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Lastname { get; set; }

        public string Name { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public int[] PermissionIdList { get; set; }

        public string Phone { get; set; }

        public string Username { get; set; }

        public string VisibleName { get; set; }


    
    
        public virtual ICollection<Role> Roles { get; set; }
    }
}