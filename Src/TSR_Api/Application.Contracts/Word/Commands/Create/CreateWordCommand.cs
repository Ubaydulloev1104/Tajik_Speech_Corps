using Application.Contracts.Converter.Converter;
using Newtonsoft.Json;

namespace Application.Contracts.Word.Commands.Create;

public class CreateWordCommand : IRequest<string>
{
    public string Value { get; set; }
    public string Description { get; set; }
    public Guid CategoryId { get; set; }
  
    [JsonConverter(typeof(DateTimeToUnixConverter))]
    public DateTime CreateDate { get; set; }
    [JsonConverter(typeof(DateTimeToUnixConverter))]
    public DateTime? UpdatedDate { get; set; }
}
