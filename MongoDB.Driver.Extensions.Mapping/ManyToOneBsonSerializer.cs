using System;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver.Builders;

namespace MongoDB.Driver.Extensions.Mapping
{
    public sealed class ManyToOneBsonSerializer : BaseBsonSerializer
    {
        public ManyToOneBsonSerializer(Type documentType, IBsonSerializer serializer, IMongoDBProvider mongoDbProvider)
            : base(documentType, serializer, mongoDbProvider)
        {

        }

        public override object Deserialize(BsonReader bsonReader, Type nominalType, Type actualType, IBsonSerializationOptions options)
        {
            object value = null;
            
            if (bsonReader.CurrentBsonType == BsonType.Null)
            {
                bsonReader.ReadNull();
            }
            else
            {
                var id = BsonValue.ReadFrom(bsonReader).AsObjectId;
                value = MongoDbProvider.Database.GetCollection(DocumentType, DocumentType.Name).FindOneAs(DocumentType, Query.EQ("_id", id));
            }
            return value;
        }

        public override void Serialize(BsonWriter bsonWriter, Type nominalType, object value, IBsonSerializationOptions options)
        {
            if (value == null)
            {
                bsonWriter.WriteNull();
            }
            else
            {
                ObjectIdSerializer.Instance.Serialize(bsonWriter, nominalType, ((IIdentifier)value).Id, options);
            }
        }
    }
}