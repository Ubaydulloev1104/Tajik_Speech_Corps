using Application.Contracts.Converter.Converter;
using Newtonsoft.Json;

namespace Application.Contracts.Word.Commands.Update
{
    public class UpdateWordCommand : IRequest<string>
    {
        public string Slug { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }

        [JsonConverter(typeof(DateTimeToUnixConverter))]
        public DateTime? UpdatedDate { get; set; }
       
       
    }
}
