using Application.Contracts.Converter.Converter;
using Application.Contracts.TimeLineDTO;
using Newtonsoft.Json;

namespace Application.Contracts.Word.Responses
{
	public class WordListDto
    {
        public Guid Id { get; set; }
        public string Category { get; set; }
        public Guid CategoryId { get; set; }
        public string Slug { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }

        [JsonConverter(typeof(DateTimeToUnixConverter))]
        public DateTime CreateDate { get; set; }
        [JsonConverter(typeof(DateTimeToUnixConverter))]
        public DateTime? UpdatedDate { get; set; }
        public ICollection<TimeLineDetailsDto> History { get; set; }
    }
}
