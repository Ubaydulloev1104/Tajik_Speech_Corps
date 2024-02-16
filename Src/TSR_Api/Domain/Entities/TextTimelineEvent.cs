using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
	public class TextTimelineEvent : TimelineEvent
	{
		public Guid TextId { get; set; }
		public Text Texts { get; set; }
	}
}
