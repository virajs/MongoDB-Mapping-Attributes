
using MongoDB.Bson;

namespace MongoDB.Driver.Extensions.Mapping
{
    public interface IIdentifier
    {
        ObjectId Id { get; set; }
    }
}
