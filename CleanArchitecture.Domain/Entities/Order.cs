namespace CleanArchitecture.Domain.Entities
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
