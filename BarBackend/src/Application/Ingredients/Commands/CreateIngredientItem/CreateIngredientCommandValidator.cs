namespace BarBackend.Application.Ingredients.Commands.CreateIngredientItem;

public class CreateIngredientCommandValidator : AbstractValidator<CreateIngredientCommand>
{
    public CreateIngredientCommandValidator()
    {
        RuleFor(v => v.Name)
            .MinimumLength(10) // just a test
            .MaximumLength(200)
            .NotEmpty();
    }
}
