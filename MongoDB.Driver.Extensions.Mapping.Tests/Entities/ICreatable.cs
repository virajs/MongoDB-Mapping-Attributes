namespace MongoDB.Driver.Extensions.Mapping.Tests.Entities
{
    public interface ICreatable
    {
        User CreatedBy { get; set; }
    }
}