namespace Application.Contracts.Applications.Commands.CreateApplication
{
	public class CreateApplicationWithoutApplicantIdCommand : IRequest<Guid>
	{
		public string CoverLetter { get; set; }
		public Guid Word { get; set; }
	}
}
