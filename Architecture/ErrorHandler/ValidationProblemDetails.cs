using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Architecture
{
	public class ValidationProblemDetails : ProblemDetails
	{
		public bool Successful => (string.IsNullOrWhiteSpace(Detail) && (ValidationErrors?.Count == 0));
		public ICollection<ValidationError> ValidationErrors { get; set; }
	}
}
