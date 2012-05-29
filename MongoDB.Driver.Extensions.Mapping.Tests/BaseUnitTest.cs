using MongoDB.Driver.Extensions.Mapping.Tests.Entities;

namespace MongoDB.Driver.Extensions.Mapping.Tests
{
    public abstract class BaseUnitTest
    {
        public User Administrator
        {
            get
            {
                return TestSetupFixture.AdminUser;
            }
        }

        public User OrderManager
        {
            get
            {
                return TestSetupFixture.OrderManager;
            }
        }

        protected IMongoDBProvider MongoDbProvider { get; private set; }

        protected void Save<T>(T document)
            where T : class 
        {
            TestSetupFixture.DefaultMongoDbProvider.Database.GetCollection<T>(typeof(T).Name).Save(document);
        }

        protected MongoCollection<T> GetCollection<T>()
        {
            return TestSetupFixture.DefaultMongoDbProvider.Database.GetCollection<T>(typeof (T).Name);
        }
    }
}
