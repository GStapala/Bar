using BarBackend.Domain.Events;
using Microsoft.Extensions.Logging;

namespace BarBackend.Application.Ingredients.EventHandlers;

public class IngredientCreatedEventHandler : INotificationHandler<IngredientCreatedEvent>
{
    private readonly ILogger<IngredientCreatedEventHandler> _logger;

    public IngredientCreatedEventHandler(ILogger<IngredientCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(IngredientCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("BarBackend Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
