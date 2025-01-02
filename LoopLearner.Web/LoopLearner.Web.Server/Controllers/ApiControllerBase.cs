namespace LoopLearner.Web.Server.Controllers;

using Domain.Errors;
using Domain.Errors.General;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;


// [Authorize]
[ApiController]
[ProducesErrorResponseType(typeof(ProblemDetails))]
[ProducesResponseType(typeof(ProblemDetails), statusCode: StatusCodes.Status500InternalServerError)]
public class ApiControllerBase : ControllerBase
{
    protected IActionResult Problem(Error error)
    {
        return error switch
        {
            ValidationError validationError => ValidationProblem(validationError),
            NotFoundError notFoundError => NotFoundProblem(notFoundError),
            BadRequestError badRequestError => BadRequestProblem(badRequestError),
            _ when error.StatusCode is not null => Problem(type: error.ErrorType, statusCode: error.StatusCode),
            _ => Problem(type: error.ErrorType, detail: error.ErrorMessage)
        };
    }

    protected IActionResult ValidationProblem(ValidationError validationError)
    {
        var modelStateDictionary = new ModelStateDictionary();
        foreach (var error in validationError.ValidationErrorDictionary)
        {
            modelStateDictionary.AddModelError(error.Key, error.Value);
        }
        return ValidationProblem(modelStateDictionary: modelStateDictionary, type: validationError.ErrorType);
    }
    protected IActionResult BadRequestProblem(BadRequestError badRequestError) => CreateProblemResponse(StatusCodes.Status400BadRequest, badRequestError.ErrorType, badRequestError.ErrorMessage);

    protected IActionResult NotFoundProblem(NotFoundError notFoundError) => CreateProblemResponse(StatusCodes.Status404NotFound, notFoundError.ErrorType, notFoundError.ErrorMessage);

    private IActionResult CreateProblemResponse(int statusCode, string type, string? message) => Problem(statusCode: statusCode, type: type, detail: message);
}