using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MongoDB.Driver.Extensions.Mapping.Tests.Entities;
using NUnit.Framework;

namespace MongoDB.Driver.Extensions.Mapping.Tests
{
    [SetUpFixture]
    public class TestSetupFixture
    {
        public static DefaultMongoDBProvider DefaultMongoDbProvider { get; private set; }

        internal static User AdminUser { get; private set; }

        internal static User OrderManager { get; private set; }

        public TestSetupFixture()
        {
            DefaultMongoDbProvider = new DefaultMongoDBProvider();
        }

        [SetUp]
        public void Setup()
        {
            var mapper = new Mapper(DefaultMongoDbProvider);
            mapper.Map(Assembly.GetExecutingAssembly());

            var adminUser = new User()
                                 {
                                     UserName = "Administrator"
                                 };
            DefaultMongoDbProvider.Database.GetCollection<User>("User").Save(adminUser);
            AdminUser = adminUser;

            var orderUser = new User()
                            {
                                UserName = "Manager - Orders",
                                CreatedBy = adminUser
                            };
            DefaultMongoDbProvider.Database.GetCollection<User>("User").Save(orderUser);
            OrderManager = orderUser;
        }

        [TearDown]
        public void Teardown()
        {
            DefaultMongoDbProvider.Database.Drop();
        }
    }
}
