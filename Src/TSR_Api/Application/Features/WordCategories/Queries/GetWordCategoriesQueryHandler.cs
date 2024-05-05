using Application.Contracts.Word.Queries.GetWordCategories;
using Application.Contracts.Word.Responses;
using Application.Contracts.WordCategore.Responses;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.WordCategories.Queries
{
    public class GetWordCategoriesQueryHandler : IRequestHandler<GetWordCategoriesQuery, List<WordCategoriesResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetWordCategoriesQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<WordCategoriesResponse>> Handle(GetWordCategoriesQuery request, CancellationToken cancellationToken)
        {

            var wordsQuery = _context.Words;
            if (request.CheckDate)
            {
                DateTime now = DateTime.Now;
              
            }
            var sortredJobs = (from j in wordsQuery
                               group j by j.CategoryId).ToList();

            var words = wordsQuery.ToList();

            var jobsWithCategory = new List<WordCategoriesResponse>();
            var categories = await _context.Categories.ToListAsync();

            foreach (var word in sortredJobs)
            {
                var category = categories.Where(c => c.Id == word.Key).FirstOrDefault();

                jobsWithCategory.Add(new WordCategoriesResponse
                {
                    CategoryId = word.Key,
                    Category = _mapper.Map<CategoryResponse>(category),
                    WordCount = word.Count()
                });
            }

            return jobsWithCategory;
        }
    }
}
