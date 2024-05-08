namespace Application.Contracts.WordCategore.Commands.CreateWordCategory;

public class CreateWordCategoryCommand : IRequest<string>
{
    public string Name { get; set; }
}
