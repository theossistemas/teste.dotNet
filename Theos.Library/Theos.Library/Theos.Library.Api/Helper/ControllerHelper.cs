using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Theos.Library.CrossCutting.Response.Error;

namespace Theos.Library.Api.Helper
{
    public static class ControllerHelper
    {
        public static ErrorResponseModel GetErrors(List<ValidationResult> errors)
        {
            return new ErrorResponseModel
            {
                Errors = errors.GroupBy(g => g.MemberNames.FirstOrDefault()).Select(s =>
                    new ItemErrorResponseModel(s.Key)
                    {
                        Messages = s.Select(sm => sm.ErrorMessage).ToList()
                    }).ToList()
            };
        }
    }
}
