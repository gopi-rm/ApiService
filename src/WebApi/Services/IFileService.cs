using Newtonsoft.Json.Linq;

namespace WebApi.Services
{
    public interface IFileService
    {
        string FileValidation(Stream stream);
    }
}
