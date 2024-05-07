using Application.Contracts.WordCategore.Commands.DeleteWordCategory;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.WordCategories.Command.DeleteWordCategory;

public class DeleteWordCategoryCommandHandler : IRequestHandler<DeleteWordCategoryCommand, bool>
{
    private readonly IApplicationDbContext _dbContext;

    public DeleteWordCategoryCommandHandler(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<bool> Handle(DeleteWordCategoryCommand request, CancellationToken cancellationToken)
    {
        var vacancyCategory = await _dbContext.Categories.FirstOrDefaultAsync(v => v.Slug == request.Slug, cancellationToken);
        if (vacancyCategory == null)
            throw new NotFoundException(nameof(WordCategory), request.Slug);

        _dbContext.Categories.Remove(vacancyCategory);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
