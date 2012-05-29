using MongoDB.Driver.Extensions.Mapping.Tests.Core.Entities;

namespace MongoDB.Driver.Extensions.Mapping.Tests.Core
{
    public sealed class DefaultIdentifierFinder : IIdentifierFinder
    {
        public object GetId(object @object)
        {
            var entity = @object as Entity;
            if (entity != null)
                return entity.Id;
            return null;
        }
    }
}
