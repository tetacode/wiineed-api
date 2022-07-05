using System;
using System.Collections.Generic;
using Core.Repository.Abstract;

namespace Data.Entity
{
    public partial class Role : IEntity
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public int[] PermissionIdList { get; set; }


    
    
        public virtual ICollection<User> Users { get; set; }
    }
}
