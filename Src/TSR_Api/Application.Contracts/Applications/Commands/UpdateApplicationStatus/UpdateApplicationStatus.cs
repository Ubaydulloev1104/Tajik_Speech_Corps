namespace Application.Contracts.Applications.Commands.UpdateApplicationStatus
{
    public class UpdateApplicationStatuss : IRequest<bool>
    {
        public string Slug { get; set; }
        public int StatusId { get; set; }
    }
}
