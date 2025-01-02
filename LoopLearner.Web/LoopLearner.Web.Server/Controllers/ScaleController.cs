using LoopLearner.Application.Scales.Queries;
using LoopLearner.Domain.Common.Entities;
using LoopLearner.Domain.ScaleAggregate.ValueObjects;
using LoopLearner.Domain.SongAggregate.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LoopLearner.Web.Server.Controllers;

[Route("api/scales")]
public class ScaleController(IMediator mediator) : ApiControllerBase
{
    [HttpGet("{rootNote}-{scaleType}")]
    [ProducesResponseType(typeof(Dictionary<ScaleBoxPosition, List<FretNote>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetScaleNotes(Note rootNote, ScaleType scaleType, CancellationToken cancellationToken)
    {
        var query = new GetScaleNotesQuery(rootNote, scaleType);
        var result = await mediator.Send(query, cancellationToken);

        if (result.IsFailure)
        {
            return Problem(result.Error);
        }

        return Ok(result.Value);
    }

}

