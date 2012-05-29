using System.Configuration;

namespace MongoDB.Driver.Extensions.Mapping.Tests
{
    public sealed class DefaultMongoDBProvider : IMongoDBProvider
    {
        private readonly MongoDatabase _database;

        public DefaultMongoDBProvider()
        {
            var connectionStringBuilder =
                        new MongoConnectionStringBuilder(ConfigurationManager.AppSettings["MongoConnectionString"]);
            var server = MongoServer.Create(connectionStringBuilder);
            _database = server.GetDatabase(connectionStringBuilder.DatabaseName);
        }

        public MongoDatabase Database
        {
            get
            {
                return _database;
            }
        }
    }
}
