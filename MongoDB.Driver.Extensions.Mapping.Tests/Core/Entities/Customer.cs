using System.Collections.Generic;

namespace MongoDB.Driver.Extensions.Mapping.Tests.Core.Entities
{
    public class Customer : Entity, ICreatable
    {
        public string Name { get; set; }

        [OneToMany(typeof(Order))]
        public IList<Order> Orders { get; set; }

        [ManyToOneAttribute(typeof(User))]
        public User CreatedBy { get; set; }

        public Customer()
        {
            Orders = new List<Order>();
        }
    }
}