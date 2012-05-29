
namespace MongoDB.Driver.Extensions.Mapping.Tests.Core.Entities
{
    public class User : Entity, ICreatable
    {
        public string UserName { get; set; }

        [ManyToOneAttribute(typeof(User))]
        public User CreatedBy { get; set; }
    }
}