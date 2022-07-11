using System;
using System.Collections.Generic;
using Core.Repository.Abstract;

namespace Data.Entity
{
    public partial class Log : MongoDbEntity
    {
        public string? Data { get; set; }
        
        public Guid? UserId { get; set; }
    }
}
