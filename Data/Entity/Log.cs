using System;
using System.Collections.Generic;
using Core.Repository.Abstract;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Data.Entity
{
    public partial class Log : MongoDbEntity
    {
        public string? Data { get; set; }
        
        
        [BsonRepresentation(BsonType.String)]
        public Guid? UserId { get; set; }
    }
}
