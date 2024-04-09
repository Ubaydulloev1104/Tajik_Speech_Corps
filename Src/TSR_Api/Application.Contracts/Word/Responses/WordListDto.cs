using Application.Contracts.Converter.Converter;
using Application.Contracts.TimeLineDTO;
using Newtonsoft.Json;
using static Application.Contracts.Dtos.Enums.ApplicationStatusDto;

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
        public int RequiredYearOfExperience { get; set; }
        public Dtos.Enums.ApplicationStatusDto.WorkSchedule WorkSchedule { get; set; }
        public ICollection<TimeLineDetailsDto> History { get; set; }
    }
}
