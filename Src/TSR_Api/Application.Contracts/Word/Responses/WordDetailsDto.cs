using Application.Contracts.Converter.Converter;
using Application.Contracts.TimeLineDTO;
using Newtonsoft.Json;
using static Application.Contracts.Dtos.Enums.ApplicationStatusDto;

namespace Application.Contracts.Word.Responses
{
    public class WordDetailsDto
    {
        public Guid Id { get; set; }
        public Guid? CategoryId { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public Dtos.Enums.ApplicationStatusDto.WorkSchedule WorkSchedule { get; set; }
        public int RequiredYearOfExperience { get; set; }

        [JsonConverter(typeof(DateTimeToUnixConverter))]
        public DateTime CreatedAt { get; set; }

        public Guid? CreatedBy { get; set; }

        [JsonConverter(typeof(DateTimeToUnixConverter))]
        public DateTime? LastModifiedAt { get; set; }

        public Guid? LastModifiedBy { get; set; }
        public ICollection<TimeLineDetailsDto> History { get; set; }

        public string Slug { get; set; }
        public string Value { get; set; }

        [JsonConverter(typeof(DateTimeToUnixConverter))]
        public DateTime CreateDate { get; set; }

    }
}
