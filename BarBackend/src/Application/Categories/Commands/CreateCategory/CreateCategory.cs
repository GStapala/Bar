using BarBackend.Application.Common.Interfaces;
using BarBackend.Domain.Entities;
using BarBackend.Domain.Events;

namespace BarBackend.Application.Categories.Commands.CreateCategory;

public record CreateCategoryCommand : IRequest<int>
{
    public required string Name { get; init; }
}

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
{
    private readonly IBarDbContext _context;

    public CreateCategoryCommandHandler(IBarDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = new Category()
        {
            Name = request.Name,
           
        };

        entity.AddDomainEvent(new CategoryCreatedEvent(entity));

        _context.Categories.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
