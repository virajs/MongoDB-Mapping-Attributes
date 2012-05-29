using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver.Builders;

namespace MongoDB.Driver.Extensions.Mapping.Serializers
{
    internal sealed class OneToManyBsonSerializer : BaseBsonSerializer
    {
        public OneToManyBsonSerializer(Type documentType, IBsonSerializer serializer, IMongoDbProvider mongoDbProvider, IIdentifierFinder identifierFinder) 
            : base(documentType, serializer, mongoDbProvider, identifierFinder)
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
                var array = value as IEnumerable;
                if (array != null)
                {
                    foreach (var id in from object arrayItem in array select IdentifierFinder.GetId(arrayItem))
                    {
                        BsonSerializer.Serialize(bsonWriter, id.GetType(), id);
                    }
                }
            }
            bsonWriter.WriteEndArray();
        }       
    }
}
