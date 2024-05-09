using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;

namespace TSR_WebUl.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AudioRecorderController : Controller
{

    private const string AudioFolderPath = "wwwroot/Audio";
    [HttpPost("SaveAudio")]
    public async Task<IActionResult> SaveAudio(IFormFile audio)
    {
        if (audio == null || audio.Length == 0)
            return BadRequest("Audio file is missing.");

        var filePath = Path.Combine(AudioFolderPath, "recording.wav");

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await audio.CopyToAsync(stream);
        }

        // Добавьте логирование для проверки
        Console.WriteLine($"Saved audio file to: {filePath}");

        return Ok();
    }

}
