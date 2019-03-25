using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Theos.Library.Api.Security;
using Theos.Library.CrossCutting;
using Theos.Library.CrossCutting.Exceptions;

namespace Theos.Library.Api.Controllers
{
    [Route("api/v1/files")]
    public class FileController : Controller
    {
        [HttpGet("{fileName}")]
        [AccessValidation]
        public async Task<IActionResult> Get(string fileName)
        {
            var filePath = $"{EnvironmentProperties.SaveFilePath}{fileName}";

            if (!System.IO.File.Exists(filePath))
                return NotFound();

            var image = System.IO.File.OpenRead(filePath);
            return File(image, $"image/{fileName.Split('.').Last()}");
        }

        [HttpPost("upload"), DisableRequestSizeLimit]
        [AccessValidation]
        public ActionResult Post(IFormFile uploadedFile)
        {
            try
            {
                var file = Request.Form.Files.FirstOrDefault();

                if (file == null || file.Length <= 0)
                    throw new EntityValidationException("Arquivo inválido ou não selecionado");

                if(!EnvironmentProperties.FormatList.Contains(file.FileName.Split('.').Last()))
                    throw new EntityValidationException("Arquivo no formato incorreto. (jpg ou png)");

                var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                var fullPath = $"{EnvironmentProperties.SaveFilePath}{fileName}";

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                return Ok(new { Url = string.Format(EnvironmentProperties.FileAccessPath, fileName), FileSize = file.Length });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
