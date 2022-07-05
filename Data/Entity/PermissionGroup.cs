using System;
using System.Collections.Generic;
using Core.Repository.Abstract;

namespace Data.Entity
{
    public partial class PermissionGroup : IEntity
    {
        public PermissionGroup()
        {
            Permissions = new HashSet<Permission>();
        }

        public int Id { get; set; }

        public string Name { get; set; }


    
    
        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
