
namespace MongoDB.Driver.Extensions.Mapping.Tests.Entities
{
    public class OrderLine : Entity
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
