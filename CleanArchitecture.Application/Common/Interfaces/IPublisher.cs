namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IPublisher<TEvent>
    {
        Task PublishAsync(TEvent @event);
    }
}
