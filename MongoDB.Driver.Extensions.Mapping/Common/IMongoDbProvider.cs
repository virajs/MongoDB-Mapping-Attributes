using System;

namespace MongoDB.Driver.Extensions.Mapping
{
    public interface IMongoDbProvider
    {
        MongoDatabase Database { get; }
    }
}
