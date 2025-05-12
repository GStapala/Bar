using BarBackend.Domain.Events;
using Microsoft.Extensions.Logging;

namespace BarBackend.Application.Categories.EventHandlers;

public class CategoryCompletedEventHandler : INotificationHandler<CategoryCompletedEvent>
{
    private readonly ILogger<CategoryCompletedEventHandler> _logger;

    public CategoryCompletedEventHandler(ILogger<CategoryCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(CategoryCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("BarBackend Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
