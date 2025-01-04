using ScaleSlayer.Application.Scales.Queries;
using ScaleSlayer.Application.Scales.Responses;
using ScaleSlayer.Domain.ScaleAggregate.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ScaleSlayer.Web.Server.Controllers;

[Route("api/scales")]
public class ScaleController(IMediator mediator) : ControllerBase
{
    [HttpGet("{rootNote}/{scaleType}")]
    [ProducesResponseType(typeof(ScaleNotesResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetScaleNotes(Note rootNote, ScaleType scaleType, CancellationToken cancellationToken)
    {
        var query = new GetScaleNotesQuery(rootNote, scaleType);
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


