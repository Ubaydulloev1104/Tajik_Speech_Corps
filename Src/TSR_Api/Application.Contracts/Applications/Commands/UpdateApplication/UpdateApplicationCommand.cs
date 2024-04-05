namespace Application.Contracts.Applications.Commands.UpdateApplication
{
    public class UpdateApplicationCommand : IRequest<Guid>
    {
        public string Slug { get; set; }
    }
}
