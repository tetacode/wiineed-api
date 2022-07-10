using System;
using System.Collections.Generic;
using Core.Repository.Abstract;

namespace Data.Entity
{
    public partial class Permission : MongoDbEntity
    {
        public Permission()
        {
        }

        public string? Description { get; set; }

        public string Name { get; set; }
    }
}
