using System;
using MongoDB.Bson.Serialization;
using MongoDB.Driver.Extensions.Mapping.Serializers;

namespace MongoDB.Driver.Extensions.Mapping.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ManyToOneAttribute : BaseMappingAttribute
    {
        public ManyToOneAttribute(Type type) 
            : base(type)
        {

        }

        public override IBsonSerializer GetBsonSerializer(IBsonSerializer serializer, IMongoDbProvider mongoDbProvider, IIdentifierFinder entityLoader)
        {
            return new ManyToOneBsonSerializer(Type, serializer, mongoDbProvider, entityLoader);
        }
    }
}