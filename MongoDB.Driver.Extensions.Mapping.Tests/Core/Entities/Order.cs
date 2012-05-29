using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver.Extensions.Mapping.Attributes;

namespace MongoDB.Driver.Extensions.Mapping.Tests.Core.Entities
{
    public class Order : Entity, ICreatable
    {
        public ObjectId CustomerId { get; set; }

        [ManyToOne(typeof(User))]
        public User CreatedBy { get; set; }

        public IList<OrderLine> Lines
        {
            get;
            set;
        }
        
        public Order()
        {
            Lines = new List<OrderLine>();
        }
    }
}