using System;
using MongoDB.Bson.Serialization;

namespace MongoDB.Driver.Extensions.Mapping.Attributes
{
    public abstract class BaseMappingAttribute : Attribute
    {
        public Type Type { get; private set; }

        protected BaseMappingAttribute(Type type)
        {
            Type = type;
        }

        public abstract IBsonSerializer GetBsonSerializer(IBsonSerializer serializer, IMongoDbProvider mongoDbProvider, IIdentifierFinder entityLoader);
    }
}