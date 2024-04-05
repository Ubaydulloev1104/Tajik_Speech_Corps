using Application.Common.SlugGeneratorService;
using Application.Contracts.WordCategore.Commands.UpdateWordCategory;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.WordCategories.Command.UpdateWordCategory
{
    public class UpdateWordCategoryCommandHandler : IRequestHandler<UpdateWordCategoryCommand, string>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ISlugGeneratorService _slugService;

        public UpdateWordCategoryCommandHandler(IApplicationDbContext context, IMapper mapper, ISlugGeneratorService slugService)
        {
            _context = context;
            _mapper = mapper;
            _slugService = slugService;
        }
        public async Task<string> Handle(UpdateWordCategoryCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Categories.FirstOrDefaultAsync(e => e.Slug == request.Slug, cancellationToken)
                ?? throw new NotFoundException(nameof(WordCategory), request.Slug);

            _mapper.Map(request, entity);
            var result = _context.Categories.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return entity.Slug;
        }
    }
}
