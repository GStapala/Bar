namespace BarBackend.Domain.Events;

public class RecipeDeletedEvent : BaseEvent
{
    public RecipeDeletedEvent(Recipe recipe)
    {
        Recipe = recipe;
    }

    public Recipe Recipe { get; }
}
