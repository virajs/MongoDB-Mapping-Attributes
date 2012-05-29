using System;

namespace MongoDB.Driver.Extensions.Mapping
{
    public interface IMongoDBProvider
    {
        MongoDatabase Database { get; }
    }
}
