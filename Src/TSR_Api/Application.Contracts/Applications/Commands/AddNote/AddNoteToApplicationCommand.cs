using Application.Contracts.TimeLineDTO;

namespace Application.Contracts.Applications.Commands.AddNote;

public class AddNoteToApplicationCommand : IRequest<TimeLineDetailsDto>
{
    public string Slug { get; set; }
    public string Note { get; set; }
}
