using AutoMapper;
using LoopLearner.Application.Scales.Queries;
using LoopLearner.Domain.ScaleAggregate.ValueObjects;
using LoopLearner.Domain.SongAggregate.Entities;
using LoopLearner.Domain.SongAggregate.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LoopLearner.Web.Server.Controllers
{
    [Route("api/scales")]
    public class ScaleController : ApiControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ScaleController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet("{rootNote}-{scaleType}")]
        [ProducesResponseType(typeof(Dictionary<ScaleBoxPosition, List<FretNote>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetScaleNotes(string rootNote, string scaleType, CancellationToken cancellationToken)
        {
            // Attempt to parse the rootNote and scaleType using the defined enums
            if (!Enum.TryParse<Note>(rootNote, true, out var note))
            {
                return BadRequest($"Invalid root note provided: '{rootNote}'. Valid values are: {string.Join(", ", Enum.GetNames(typeof(Note)))}.");
            }

            if (!Enum.TryParse<ScaleType>(scaleType, true, out var scale))
            {
                return BadRequest($"Invalid scale type provided: '{scaleType}'. Valid values are: {string.Join(", ", Enum.GetNames(typeof(ScaleType)))}.");
            }

            // Create and send the query to get scale notes
            var query = new GetScaleNotesQuery(note, scale);
            var result = await _mediator.Send(query, cancellationToken);

            // Check for failure and return the appropriate response
            if (result.IsFailure)
            {
                return Problem(result.Error);
            }

            // Return the successful result
            return Ok(result.Value);
        }

        
        [HttpGet("csharp-minor-pentatonic-chords")]
        [ProducesResponseType(typeof(List<Chord>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCSharpMinorPentatonicChords(CancellationToken cancellationToken)
        {
            var query = new GetScaleChordsQuery(Note.CSharp, ScaleType.PentatonicMinor);
            var result = await _mediator.Send(query, cancellationToken);

            if (result.IsFailure)
            {
                return Problem(result.Error);
            }

            return Ok(result.Value);
        }
    }
}