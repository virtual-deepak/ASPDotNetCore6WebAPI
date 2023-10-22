using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace WorkingWithFiles.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilesController : ControllerBase
{
    private readonly FileExtensionContentTypeProvider _fileExtensionProvider; 
    public FilesController(FileExtensionContentTypeProvider fileExtensionProvider)
    {
        _fileExtensionProvider = fileExtensionProvider;
    }

    [HttpGet("{fileId}")]
    public ActionResult GetFile(int fileId)
    {
        var pathToFile = "getting-started-with-rest-slides.pdf";
        if (!System.IO.File.Exists(pathToFile))
        {
            return NotFound();
        }

        if (!_fileExtensionProvider.TryGetContentType(pathToFile, out string contentType))
        {
            contentType = "application/octext-stream";
        }

        var bytes = System.IO.File.ReadAllBytes(pathToFile);
        return File(bytes, contentType, Path.GetFileName(pathToFile));
    }
}
