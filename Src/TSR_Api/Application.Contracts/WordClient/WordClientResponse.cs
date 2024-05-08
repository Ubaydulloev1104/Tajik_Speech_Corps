namespace Application.Contracts.WordClient;

public class WordClientResponse
{
    public string Category { get; set; }
    public string Value { get; set; }
    public string Description { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string Slug { get; set; }
}
