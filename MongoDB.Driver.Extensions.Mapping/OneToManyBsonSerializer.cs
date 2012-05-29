using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver.Builders;

namespace MongoDB.Driver.Extensions.Mapping
{
    public sealed class OneToManyBsonSerializer : BaseBsonSerializer
    {
        public OneToManyBsonSerializer(Type documentType, IBsonSerializer serializer, IMongoDBProvider mongoDbProvider)
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
                bsonReader.ReadStartArray();
                var idList = new List<ObjectId>();
                while (bsonReader.ReadBsonType() != BsonType.EndOfDocument)
                {
                    var id = (ObjectId)BsonSerializer.Deserialize(bsonReader, typeof(ObjectId));
                    idList.Add(id);
                }
                bsonReader.ReadEndArray();

                if (idList.Count > 0)
                {
                    var cursor = MongoDbProvider.Database.GetCollection(DocumentType, DocumentType.Name)
                        .FindAs(DocumentType, Query.In("_id", BsonArray.Create(idList)));

                    var documents = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(DocumentType));
                    foreach (var document in cursor)
                    {
                        documents.Add(document);
                    }
                    value = documents;
                }               
            }
            return value;
        }
        
        public override void Serialize(BsonWriter bsonWriter, Type nominalType, object value, IBsonSerializationOptions options)
        {
            bsonWriter.WriteStartArray();
            if (value != null)
            {
                var values = value as IEnumerable<IIdentifier>;
                if(values != null)
                {
                    foreach (object id in values.Select(p => p.Id).ToList())
                    {
                        BsonSerializer.Serialize(bsonWriter, typeof(ObjectId), id);
                    }
                }
            }
            bsonWriter.WriteEndArray();
        }       
    }
}
