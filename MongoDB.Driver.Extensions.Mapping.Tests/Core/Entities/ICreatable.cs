namespace MongoDB.Driver.Extensions.Mapping.Tests.Core.Entities
{
    public interface ICreatable
    {
        User CreatedBy { get; set; }
    }
}