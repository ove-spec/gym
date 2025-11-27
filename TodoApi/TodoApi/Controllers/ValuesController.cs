using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ImageController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ImageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("fetch")]
        public async Task<IActionResult> FetchImage([FromQuery] string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return BadRequest(new { error = "Ingen URL angiven" });
            }

            try
            {
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, new { error = "Kunde inte hämta bilden" });
                }

                var contentType = response.Content.Headers.ContentType?.MediaType ?? "application/octet-stream";
                var imageStream = await response.Content.ReadAsStreamAsync();

                return File(imageStream, contentType);
            }
            catch (HttpRequestException e)
            {
                return StatusCode(500, new { error = e.Message });
            }
        }
    }
}
