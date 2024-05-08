namespace Application.Contracts.WordCategore.Commands.UpdateWordCategory;

public class UpdateWordCategoryCommand : IRequest<string>
{
    public string Slug { get; set; }
    public string Name { get; set; }
}
