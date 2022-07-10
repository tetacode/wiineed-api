using System;
using System.Collections.Generic;
using Core.Repository.Abstract;

namespace Data.Entity
{
    public partial class Role : MongoDbEntity
    {
        public Role()
        {
            
        }

        public string Name { get; set; }

        public string[] PermissionIdList { get; set; }
    }
}
