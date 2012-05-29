using MongoDB.Bson;

namespace MongoDB.Driver.Extensions.Mapping.Tests.Core.Entities
{
    public abstract class Entity
    {
        public ObjectId Id { get; set; }
    }
}