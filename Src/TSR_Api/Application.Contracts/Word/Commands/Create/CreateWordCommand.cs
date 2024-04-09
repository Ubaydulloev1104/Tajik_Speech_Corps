using Application.Contracts.Converter.Converter;
using Newtonsoft.Json;
using static Application.Contracts.Dtos.Enums.ApplicationStatusDto;

namespace Application.Contracts.Word.Commands.Create
{
    public class CreateWordCommand : IRequest<string>
    {
        public string Slug { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
      
        [JsonConverter(typeof(DateTimeToUnixConverter))]
        public DateTime CreateDate { get; set; }
        [JsonConverter(typeof(DateTimeToUnixConverter))]
        public DateTime? UpdatedDate { get; set; }
        public int RequiredYearOfExperience { get; set; }
        public WorkSchedule WorkSchedule { get; set; }
    }
}
}
