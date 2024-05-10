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

    public async Task<IActionResult> SaveAudio(IFormFile audio, string nameaudio)
    {
        if (audio == null || audio.Length == 0)
            return BadRequest("Audio file is missing.");

        // Проверяем, задано ли имя аудиофайла
        if (string.IsNullOrEmpty(nameaudio))
            return BadRequest("Audio file name is missing.");

        var fileName = $"{nameaudio}.wav"; // Формируем имя файла с расширением .wav
        var filePath = Path.Combine(AudioFolderPath, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await audio.CopyToAsync(stream);
        }

        // Добавьте логирование для проверки
        Console.WriteLine($"Saved audio file to: {filePath}");

        return Ok();
    }
    [HttpGet("{nameaudio}")]
    public IActionResult GetAudio(string nameaudio)
    {
        var fileName = $"{nameaudio}.wav";
        var filePath = Path.Combine(AudioFolderPath, fileName);

        if (!System.IO.File.Exists(filePath))
            return NotFound();

        var memory = new MemoryStream();
        using (var stream = new FileStream(filePath, FileMode.Open))
        {
            stream.CopyTo(memory);
        }
        memory.Position = 0;

        return File(memory, "audio/wav", fileName);
    }
    [HttpPut("{nameaudio}")]
    public async Task<IActionResult> UpdateAudio(string nameaudio, IFormFile audio)
    {
        if (audio == null || audio.Length == 0)
            return BadRequest("Audio file is missing.");

        if (string.IsNullOrEmpty(nameaudio))
            return BadRequest("Audio file name is missing.");

        var fileName = $"{nameaudio}.wav";
        var filePath = Path.Combine(AudioFolderPath, fileName);

        if (!System.IO.File.Exists(filePath))
            return NotFound();

        // Перезаписываем файл
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await audio.CopyToAsync(stream);
        }

        // Добавьте логирование для проверки
        Console.WriteLine($"Updated audio file: {filePath}");

        return Ok();
    }
    [HttpDelete("{nameaudio}")]
    public IActionResult DeleteAudio(string nameaudio)
    {
        var fileName = $"{nameaudio}.wav";
        var filePath = Path.Combine(AudioFolderPath, fileName);

        if (!System.IO.File.Exists(filePath))
            return NotFound();

        System.IO.File.Delete(filePath);

        // Добавьте логирование для проверки
        Console.WriteLine($"Deleted audio file: {filePath}");

        return NoContent();
    }
    [HttpGet]
    public IActionResult GetAllAudio()
    {
        var audioFiles = Directory.GetFiles(AudioFolderPath)
                                  .Select(Path.GetFileNameWithoutExtension)
                                  .ToList();

        return Ok(audioFiles);
    }

}
