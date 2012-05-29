using System;
using MongoDB.Bson.Serialization;

namespace MongoDB.Driver.Extensions.Mapping
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