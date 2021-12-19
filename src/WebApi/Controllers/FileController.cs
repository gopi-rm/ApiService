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
        public string Post(IFormFile formFile)
        {
            return _fileService.FileValidation(formFile.OpenReadStream());
        }


    }
}
