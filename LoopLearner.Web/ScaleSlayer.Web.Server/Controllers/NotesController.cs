using ScaleSlayer.Application.Notes.Queries;
using ScaleSlayer.Application.Scales.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ScaleSlayer.Web.Server.Controllers;

[Route("api/notes")]
public class NotesController(IMediator mediator) : ControllerBase
{
    [HttpGet("notes")]
    [ProducesResponseType(typeof(GetAllNotesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAllNotes(CancellationToken cancellationToken)
    {
        var query = new GetAllNotesQuery();
        var result = await mediator.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return NotFound(new ProblemDetails
            {
                Title = "Scale not found",
                Detail = result.Error
            });
        }
    
        return Ok(result.Value);
    }
}