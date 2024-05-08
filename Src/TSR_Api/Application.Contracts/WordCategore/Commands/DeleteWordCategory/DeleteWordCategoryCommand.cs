namespace Application.Contracts.WordCategore.Commands.DeleteWordCategory;

public class DeleteWordCategoryCommand : IRequest<bool>
{
    public string Slug { get; set; }
}
