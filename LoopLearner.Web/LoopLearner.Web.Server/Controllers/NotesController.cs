using LoopLearner.Application.Notes.Queries;
using LoopLearner.Domain.Common.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LoopLearner.Web.Server.Controllers;

[Route("api/notes")]
public class NotesController(IMediator mediator) : ApiControllerBase
{
    [HttpGet("notes")]
    [ProducesResponseType(typeof(IEnumerable<FretNote>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAllNotes(CancellationToken cancellationToken)
    {
        var query = new GetAllNotesQuery();
        var result = await mediator.Send(query, cancellationToken);

        if (result.IsFailure)
            return Problem(result.Error);

        return Ok(result.Value);
    }
}