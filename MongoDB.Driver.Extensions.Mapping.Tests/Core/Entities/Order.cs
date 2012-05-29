using System.Collections.Generic;
using MongoDB.Bson;

namespace MongoDB.Driver.Extensions.Mapping.Tests.Core.Entities
{
    public class Order : Entity, ICreatable
    {
        public ObjectId CustomerId { get; set; }

        [ManyToOneAttribute(typeof(User))]
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