using System;
using System.Collections.Generic;
using Core.Repository.Abstract;

namespace Data.Entity
{
    public partial class PermissionGroup : MongoDbEntity
    {
        public PermissionGroup()
        {
            
        }

        public string Name { get; set; }
    }
}
