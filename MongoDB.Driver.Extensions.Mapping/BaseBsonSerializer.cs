using System;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;

namespace MongoDB.Driver.Extensions.Mapping
{
    public abstract class BaseBsonSerializer : IBsonSerializer
    {
        protected Type DocumentType { get; private set; }

        protected IMongoDBProvider MongoDbProvider { get; private set; }

        protected IBsonSerializer Serializer { get; private set; }

        protected BaseBsonSerializer(Type documentType, IBsonSerializer serializer, IMongoDBProvider mongoDbProvider)
        {
            DocumentType = documentType;
            Serializer = serializer;
            MongoDbProvider = mongoDbProvider;
        }

        public object Deserialize(BsonReader bsonReader, Type nominalType, IBsonSerializationOptions options)
        {
            return Deserialize(bsonReader, nominalType, nominalType, options);
        }

        public bool GetDocumentId(object document, out object id, out Type idNominalType, out IIdGenerator idGenerator)
        {
            var bsonSerializable = (IBsonSerializable)document;
            return bsonSerializable.GetDocumentId(out id, out idNominalType, out idGenerator);
        }

        public void SetDocumentId(object document, object id)
        {
            var bsonSerializable = (IBsonSerializable)document;
            bsonSerializable.SetDocumentId(id);
        }

        public IBsonSerializationOptions GetDefaultSerializationOptions()
        {
            return Serializer.GetDefaultSerializationOptions();
        }

        public BsonSerializationInfo GetItemSerializationInfo()
        {
            return Serializer.GetItemSerializationInfo();
        }

        public BsonSerializationInfo GetMemberSerializationInfo(string memberName)
        {
            return Serializer.GetMemberSerializationInfo(memberName);
        }

        public abstract object Deserialize(BsonReader bsonReader, Type nominalType, Type actualType, IBsonSerializationOptions options);

        public abstract void Serialize(BsonWriter bsonWriter, Type nominalType, object value, IBsonSerializationOptions options);
    }
}