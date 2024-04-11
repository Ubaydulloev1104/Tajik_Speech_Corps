namespace Domain.Entities
{
    public class WordTimelineEvent : TimelineEvent
	{
		public Guid WordId { get; set; }
		public Words Words { get; set; }
	}
}
