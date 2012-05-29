
namespace MongoDB.Driver.Extensions.Mapping
{
    public interface IIdentifierFinder
    {
        object GetId(object @object);
    }
}
