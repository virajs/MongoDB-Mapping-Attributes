using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MongoDB.Bson.Serialization;

namespace MongoDB.Driver.Extensions.Mapping
{
    public sealed class Mapper
    {
        private readonly IMongoDbProvider _mongoDbProvider;

        private readonly IIdentifierFinder _entityLoader;

        public Mapper(IMongoDbProvider mongoDbProvider, IIdentifierFinder entityLoader)
        {
            _entityLoader = entityLoader;
            _mongoDbProvider = mongoDbProvider;
        }

        public void Map(Assembly assembly)
        {
            IList<Type> mappedEntityTypes =
                assembly.GetTypes()
                    .Where(p => Attribute.IsDefined(p, typeof (ClassAttribute)) && !p.IsInterface && !p.IsValueType && !p.IsAbstract)
                    .ToList();
            foreach (var mappedEntityType in mappedEntityTypes)
            {
                Map(mappedEntityType);
            }
        }

        private void Map(Type documentType)
        {
            var properties =
                documentType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            if (properties.Length <= 0)
            {
                return;
            }

            foreach (var property in properties)
            {
                RegisterCustomSerializer(property, typeof (ManyToOneAttribute), documentType);
                RegisterCustomSerializer(property, typeof(OneToManyAttribute), documentType);
            }
        }

        private void RegisterCustomSerializer(PropertyInfo property, Type attrbuteType, Type documentType)
        {
            var mappingAttrbs = property.GetCustomAttributes(attrbuteType, false);
            if(mappingAttrbs.Length == 1)
            {
                var mappingAttribute = mappingAttrbs[0] as BaseMappingAttribute;
                if(mappingAttribute != null)
                {
                    var serializer = BsonSerializer.LookupSerializer(documentType);

                    BsonClassMap.LookupClassMap(documentType)
                            .GetMemberMap(property.Name)
                            .SetSerializer(mappingAttribute.GetBsonSerializer(serializer, _mongoDbProvider, _entityLoader));
                }
            }
        }

        
    }
}