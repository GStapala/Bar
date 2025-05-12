namespace BarBackend.Application.Recipes.Commands.UpdateRecipe;

public class UpdateRecipeCommandValidator : AbstractValidator<UpdateRecipeCommand>
{
    public UpdateRecipeCommandValidator()
    {
        RuleFor(v => v.Name)
            .MinimumLength(30) // just a test
            .MaximumLength(200)
            .NotEmpty();
    }
}
