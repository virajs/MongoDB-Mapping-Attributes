using System;
using MongoDB.Bson.Serialization;
using MongoDB.Driver.Extensions.Mapping.Serializers;

namespace MongoDB.Driver.Extensions.Mapping.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class OneToManyAttribute : BaseMappingAttribute
    {
        public OneToManyAttribute(Type type) 
            : base(type)
        {

        }

        public override IBsonSerializer GetBsonSerializer(IBsonSerializer serializer, IMongoDbProvider mongoDbProvider, IIdentifierFinder entityLoader)
        {
            return new OneToManyBsonSerializer(Type, serializer, mongoDbProvider, entityLoader);
        }
    }
}