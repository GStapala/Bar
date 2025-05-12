using BarBackend.Domain.Events;
using Microsoft.Extensions.Logging;

namespace BarBackend.Application.Recipes.EventHandlers;

public class RecipeCompletedEventHandler : INotificationHandler<RecipeCompletedEvent>
{
    private readonly ILogger<RecipeCompletedEventHandler> _logger;

    public RecipeCompletedEventHandler(ILogger<RecipeCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(RecipeCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("BarBackend Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
