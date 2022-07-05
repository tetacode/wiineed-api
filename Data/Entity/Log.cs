using System;
using System.Collections.Generic;
using Core.Repository.Abstract;

namespace Data.Entity
{
    public partial class Log : IEntity
    {
        public long Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public string? Data { get; set; }

        public Guid? UserId { get; set; }

    }
}
