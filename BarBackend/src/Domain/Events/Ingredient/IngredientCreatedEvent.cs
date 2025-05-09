namespace BarBackend.Domain.Events;

public class IngredientCreatedEvent : BaseEvent
{
    public IngredientCreatedEvent(Ingredient ingredient)
    {
        Ingredient = ingredient;
    }

    public Ingredient Ingredient { get; }
}
