namespace BarBackend.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(v => v.Name)
            .MinimumLength(10) // just a test
            .MaximumLength(200)
            .NotEmpty();
    }
}
