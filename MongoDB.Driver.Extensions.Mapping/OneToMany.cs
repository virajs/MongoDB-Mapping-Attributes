using System;
using MongoDB.Bson.Serialization;

namespace MongoDB.Driver.Extensions.Mapping
{
    public abstract class MappingAttribute : Attribute
    {
        public Type Type { get; private set; }

        protected MappingAttribute(Type type)
        {
            Type = type;
        }

        public abstract IBsonSerializer GetBsonSerializer(IBsonSerializer serializer, IMongoDBProvider mongoDbProvider);
    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class OneToMany : MappingAttribute
    {
        public OneToMany(Type type) 
            : base(type)
        {

        }

        public override IBsonSerializer GetBsonSerializer(IBsonSerializer serializer, IMongoDBProvider mongoDbProvider)
        {
            return new OneToManyBsonSerializer(Type, serializer, mongoDbProvider);
        }
    }
}