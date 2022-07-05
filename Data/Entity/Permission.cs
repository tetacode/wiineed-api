using System;
using System.Collections.Generic;
using Core.Repository.Abstract;

namespace Data.Entity
{
    public partial class Permission : IEntity
    {
        public Permission()
        {
            InverseParentPermission = new HashSet<Permission>();
        }

        public int Id { get; set; }

        public DateTime? CreatedTime { get; set; }

        public int[]? Dependencies { get; set; }

        public string? Description { get; set; }

        public string Name { get; set; }

        public int? ParentPermissionId { get; set; }

        public int? PermissionGroup { get; set; }


    
    
        public virtual Permission? ParentPermission { get; set; }
        public virtual PermissionGroup? PermissionGroupNavigation { get; set; }
        public virtual ICollection<Permission> InverseParentPermission { get; set; }
    }
}
