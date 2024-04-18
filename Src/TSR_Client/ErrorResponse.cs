using System.Collections.Generic;

namespace TSR_Client
{
    public class ErrorResponse
    {
        public Dictionary<string, string[]> Errors { get; set; } = new Dictionary<string, string[]>();
    }
}
