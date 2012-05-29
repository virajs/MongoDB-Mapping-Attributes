using System;
using MongoDB.Bson.Serialization;

namespace MongoDB.Driver.Extensions.Mapping
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ManyToOne : MappingAttribute
    {
        public ManyToOne(Type type) 
            : base(type)
        {

        }

        public override IBsonSerializer GetBsonSerializer(IBsonSerializer serializer, IMongoDBProvider mongoDbProvider)
        {
            return new ManyToOneBsonSerializer(Type, serializer, mongoDbProvider);
        }
    }
}