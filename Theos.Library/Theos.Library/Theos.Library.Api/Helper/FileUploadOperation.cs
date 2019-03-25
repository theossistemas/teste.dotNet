using System.Linq;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Theos.Library.Api.Helper
{
    public class FileUploadOperation : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.OperationId.ToLower() == "apiv1filesuploadpost")
            {
                operation.Parameters.Clear();
                //var fileParameter = operation.Parameters.FirstOrDefault(f => f.Name.ToLower() == "file");
                //operation.Parameters.Remove(fileParameter);

                //operation.Parameters.Where(w => w.In.ToLower() == "formdata").ToList().ForEach(f => operation.Parameters.Remove(f));
                
                operation.Parameters.Add(new NonBodyParameter
                {
                    Name = "uploadedFile",
                    In = "formData",
                    Description = "Upload File",
                    Required = false,
                    Type = "file"
                });
                operation.Consumes.Add("multipart/form-data");
            }
        }
    }
}
