using System.Collections.Generic;
using MongoDB.Driver.Extensions.Mapping.Attributes;

namespace MongoDB.Driver.Extensions.Mapping.Tests.Core.Entities
{
    public class Customer : Entity, ICreatable
    {
        public string Name { get; set; }

        [OneToMany(typeof(Order))]
        public IList<Order> Orders { get; set; }

        [ManyToOne(typeof(User))]
        public User CreatedBy { get; set; }

        public Customer()
        {
            Orders = new List<Order>();
        }
    }
}