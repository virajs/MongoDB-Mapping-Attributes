
using MongoDB.Driver.Extensions.Mapping.Attributes;

namespace MongoDB.Driver.Extensions.Mapping.Tests.Core.Entities
{
    public class User : Entity, ICreatable
    {
        public string UserName { get; set; }

        [ManyToOne(typeof(User))]
        public User CreatedBy { get; set; }
    }
}