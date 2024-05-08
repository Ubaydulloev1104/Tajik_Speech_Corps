using Application.Contracts.Word.Responses;

namespace Application.Contracts.Word.Queries.GetWordBySlug;

public class GetWordBySlugQuery : IRequest<WordDetailsDto>
{
    public string Slug { get; set; }
}
