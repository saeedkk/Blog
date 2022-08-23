using Microsoft.AspNetCore.Http;

namespace Numito_Blog.CoreLayer.Services.FileManager
{
    public interface IFileManager
    {
        string SaveFileAndReturnName(IFormFile file, string savePath);
    }
}