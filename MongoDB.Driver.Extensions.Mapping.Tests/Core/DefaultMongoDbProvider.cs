using System.Configuration;

namespace MongoDB.Driver.Extensions.Mapping.Tests.Core
{
    public sealed class DefaultMongoDbProvider : IMongoDbProvider
    {
        private readonly MongoDatabase _database;

        public DefaultMongoDbProvider()
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
