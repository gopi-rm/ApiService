using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WebApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }
        // POST api/<FileController>
        [HttpPost("api/file/validation")]
        [ProducesResponseType(StatusCodes.Status415UnsupportedMediaType)]
        public IActionResult Post(IFormFile formFile)
        {
            if (formFile.ContentType != "text/plain")
                return new UnsupportedMediaTypeResult();
            return Ok(_fileService.FileValidation(formFile.OpenReadStream()));
        }


    }
}
