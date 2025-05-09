namespace BarBackend.Domain.Events;

public class CategoryCompletedEvent : BaseEvent
{
    public CategoryCompletedEvent(Category category)
    {
        Category = category;
    }

    public Category Category { get; }
}
