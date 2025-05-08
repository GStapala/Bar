using BarBackend.Application.Ingredients.Commands.UpdateIngredient;

namespace BarBackend.Application.TodoItems.Commands.UpdateTodoItem;

public class UpdateIngredientCommandValidator : AbstractValidator<UpdateIngredientCommand>
{
    public UpdateIngredientCommandValidator()
    {
        RuleFor(v => v.Name)
            .MinimumLength(30) // just a test
            .MaximumLength(200)
            .NotEmpty();
    }
}
