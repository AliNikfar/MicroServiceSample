using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel;
using Microsoft.IdentityModel.SecurityTokenService;
using System.Net;


namespace Ordering.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : Controller
    {
        private readonly ProblemDetailsFactory _problemDetailsFactory;
         
        public ErrorController( ProblemDetailsFactory problemDetailsFactory)
        {
            _problemDetailsFactory = problemDetailsFactory;
        }
        [Route("/errors")]
        public IActionResult Errors()
        {
            var exceptionHandler = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = exceptionHandler.Error;

            var problem = new ProblemDetails();
            problem.Instance = exceptionHandler.Path;
            problem.Detail = exception.Message;
            problem.Status = (int)HttpStatusCode.InternalServerError;

            //switch(exception)
            //{
            //    case BadRequestException badRequestException:

            //        foreach(var validationerror in badRequestException.Message)
            //        {
            //            problem.Extensions.Add(validationerror);
            //        }
            //        break;


            //}
            return new JsonResult(new { errorMessage = exception.Message });
            //return MyProblem(statusCode: (int)HttpStatusCode.InternalServerError);
        }
    //    public ObjectResult MyProblem(ProblemDetails problem)
    //    {
    //        ProblemDetails problemDetails;
    //        if (problemDetails == null)
    //        {
    //            // ProblemDetailsFactory may be null in unit testing scenarios. Improvise to make this more testable.
    //            problemDetails = new ProblemDetails
    //            {
    //                Detail = detail,
    //                Instance = instance,
    //                Status = statusCode ?? 500,
    //                Title = title,
    //                Type = type,
    //            };
    //        }
    //        else
    //        {
    //            problemDetails = ProblemDetailsFactory.CreateProblemDetails(
    //                HttpContext,
    //                statusCode: statusCode ?? 500,
    //                title: title,
    //                type: type,
    //                detail: detail,
    //                instance: instance);
    //        }

    //        if (extensions is not null)
    //        {
    //            foreach (var extension in extensions)
    //            {
    //                problemDetails.Extensions.Add(extension);
    //            }
    //        }

    //        return new ObjectResult(problemDetails)
    //        {
    //            StatusCode = problemDetails.Status
    //        };
    //    }

    }
}
