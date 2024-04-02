namespace Application.Contracts.Applications.Commands.CreateApplication
{
	public class CreateApplicationCommand : IRequest<Guid>
	{
		public Guid WordId { get; set; }
		

	}
}
