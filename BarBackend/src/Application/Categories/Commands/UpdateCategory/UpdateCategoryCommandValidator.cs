namespace BarBackend.Application.Categories.Commands.UpdateCategory;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
    public UpdateCategoryCommandValidator()
    {
        RuleFor(v => v.Name)
            .MinimumLength(30) // just a test
            .MaximumLength(200)
            .NotEmpty();
    }
}
