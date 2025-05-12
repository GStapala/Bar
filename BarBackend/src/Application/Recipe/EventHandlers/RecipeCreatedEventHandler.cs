using BarBackend.Domain.Events;
using Microsoft.Extensions.Logging;

namespace BarBackend.Application.Recipes.EventHandlers;

public class RecipeCreatedEventHandler : INotificationHandler<RecipeCreatedEvent>
{
    private readonly ILogger<RecipeCreatedEventHandler> _logger;

    public RecipeCreatedEventHandler(ILogger<RecipeCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(RecipeCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("BarBackend Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
