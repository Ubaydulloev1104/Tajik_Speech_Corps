using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class WordTimelineEvent : TimelineEvent
	{
		public Guid TextId { get; set; }
		public Word Texts { get; set; }
	}
}
