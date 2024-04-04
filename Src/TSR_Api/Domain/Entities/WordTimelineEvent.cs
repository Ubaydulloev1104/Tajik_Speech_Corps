namespace Domain.Entities
{
    public class WordTimelineEvent : TimelineEvent
	{
		public Guid WordId { get; set; }
		public Word Words { get; set; }
	}
}
