using MongoDB.Bson;

namespace MongoDB.Driver.Extensions.Mapping.Tests.Entities
{
    public class Entity : IIdentifier
    {
        public ObjectId Id { get; set; }
    }
}