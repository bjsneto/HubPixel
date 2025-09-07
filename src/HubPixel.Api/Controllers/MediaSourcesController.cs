using HubPixel.Application.MediaSource;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace HubPixel.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MediaSourcesController : ControllerBase
{
    private readonly IMediaSourceService _mediaSourceService;
    private readonly IM3u8ParserService _m3u8ParserService;

    public MediaSourcesController(IMediaSourceService mediaSourceService, IM3u8ParserService m3u8ParserService)
    {
        _mediaSourceService = mediaSourceService;
        _m3u8ParserService = m3u8ParserService;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateMediaSourceInput input, CancellationToken cancellationToken)
    {
        try
        {
            await _mediaSourceService.CreateAsync(input, cancellationToken);
            return CreatedAtAction(nameof(Get), input);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public IActionResult Get(Guid id)
    {
        return Ok();
    }


    [HttpPost("parse-test")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ParseM3u8File([FromBody] M3u8FileModel input)
    {
        if (string.IsNullOrEmpty(input?.FileContentBase64))
        {
            return BadRequest("O conteúdo do arquivo em Base64 não foi fornecido ou está vazio.");
        }

        try
        {
            byte[] fileBytes = Convert.FromBase64String(input.FileContentBase64);

            using var stream = new MemoryStream(fileBytes);
            using var reader = new StreamReader(stream, Encoding.UTF8);
            string fileContent = await reader.ReadToEndAsync();

            var (channels, categories) = _m3u8ParserService.ParseFile(fileContent);

            var result = new
            {
                TotalChannels = channels.Count,
                TotalCategories = categories.Count,
                Categories = categories.Select(c => new { c.Id, c.Name, c.Description }),
                Channels = channels.Select(c => new { c.Id, c.Name, c.Description, Url = c.UrlStream.Value, CategoryIds = c.CategoryIds })
            };

            return Ok(result);
        }
        catch (FormatException)
        {
            return BadRequest("A string fornecida não é um Base64 válido.");
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao processar o arquivo: {ex.Message}");
        }
    }
}
