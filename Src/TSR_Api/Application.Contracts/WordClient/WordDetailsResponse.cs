namespace Application.Contracts.WordClient;

public class WordDetailsResponse
{
    public string Value { get; set; }
    public string Description { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public DateTime Deadline { get; set; }
	public bool IsApplied { get; set; }
}
