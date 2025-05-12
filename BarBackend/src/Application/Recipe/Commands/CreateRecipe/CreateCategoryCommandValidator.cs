namespace BarBackend.Application.Recipes.Commands.CreateRecipe;

public class CreateRecipeCommandValidator : AbstractValidator<CreateRecipeCommand>
{
    public CreateRecipeCommandValidator()
    {
        RuleFor(v => v.Name)
            .MinimumLength(10) // just a test
            .MaximumLength(200)
            .NotEmpty();
    }
}
