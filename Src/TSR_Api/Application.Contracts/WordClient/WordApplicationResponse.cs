using Application.Contracts.Dtos;

namespace Application.Contracts.WordClient;

public class WordApplicationResponse
{
    public Guid Id { get; set; }
    public string Value { get; set; }
    public string Description { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public IEnumerable<WordResponseAudioDto> WordResponseAudioDto { get; set; }
}
