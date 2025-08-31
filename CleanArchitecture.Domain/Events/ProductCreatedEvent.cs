namespace CleanArchitecture.Domain.Events
{
    public class ProductCreatedEvent
    {
        public int ProductId { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
