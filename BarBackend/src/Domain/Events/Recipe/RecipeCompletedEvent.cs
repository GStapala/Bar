namespace BarBackend.Domain.Events;

public class RecipeCompletedEvent : BaseEvent
{
    public RecipeCompletedEvent(Recipe recipe)
    {
        Recipe = recipe;
    }

    public Recipe Recipe { get; }
}
