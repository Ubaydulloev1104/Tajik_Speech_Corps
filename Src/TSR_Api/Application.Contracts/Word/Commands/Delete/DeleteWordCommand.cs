namespace Application.Contracts.Word.Commands.Delete
{
    public class DeleteWordCommand : IRequest<bool>
    {
        public string Slug { get; set; }
    }
}

