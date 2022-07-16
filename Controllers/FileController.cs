
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace DotNetCoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/file")]
    public class FileController : ControllerBase
    {
        private readonly FileExtensionContentTypeProvider fileExtensionContentTypeProvider;

        public FileController(FileExtensionContentTypeProvider fileExtensionContentTypeProvider)
        {
            this.fileExtensionContentTypeProvider = fileExtensionContentTypeProvider;
        }

        [HttpGet]
        [Route("download")]
        public IActionResult DownloadFile()
        {
            var filePath = @"Content\sample.pdf";
            fileExtensionContentTypeProvider.TryGetContentType(filePath, out string contentType);

            if (contentType == null)
            {
                contentType = MediaTypeNames.Application.Octet;
            }

            return File(System.IO.File.ReadAllBytes(filePath), contentType);
        }
    }
}