using AutoMapper;
using CSharpFunctionalExtensions;
using LoopLearner.Application.Songs.CreateSong;
using LoopLearner.Application.Songs.Queries;
using LoopLearner.Domain.Errors;
using LoopLearner.Domain.SongAggregate;
using LoopLearner.Domain.SongAggregate.Entities;
using LoopLearner.Infrastructure.Persistence;
using LoopLearner.Web.Server.Controllers;
using LoopLearner.Web.Server.Dto;
using LoopLearner.Web.Server.Requests;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LoopLearner.Web.Server;

[Route("api/songs")]
public class SongController : ApiControllerBase
{
    private readonly LoopLearnerDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public SongController(IMapper mapper, IMediator mediator, LoopLearnerDbContext context)
    {
        _mapper = mapper;
        _mediator = mediator;
        _context = context;
    }

    [Authorize]
    [HttpPost]
    [ProducesResponseType(typeof(SongDto), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateSong([FromBody] CreateSongRequest songToAdd)
    {
        var command = new CreateSongCommand(
            songToAdd.Title,
            songToAdd.Artist,
            songToAdd.Genre,
            songToAdd.BPM,
            songToAdd.InstrumentTabs.Select(a =>
                new InstrumentPartCommand(a.Id, a.InstrumentName, _mapper.Map<List<Tab>>(a.Tabs))));

        Result<Song, Error> result = await _mediator.Send(command);

        if (result.IsFailure)
            return Problem(result.Error);

        return CreatedAtRoute("GetSongById", new { id = result.Value.Id.Value }, _mapper.Map<Song>(result.Value));
    }

    // New Endpoint: Get All Chords
    [HttpGet("chords")]
    [ProducesResponseType(typeof(IEnumerable<Chord>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Chord>>> GetAllChords()
    {
        var chords = await _context.Chords
            .Include(c => c.Notes)
            .ThenInclude(n => n.Position) // Include the NotePosition for each Note
            .ToListAsync();

        return Ok(chords);
    }

    [HttpGet("notes")]
    [ProducesResponseType(typeof(IEnumerable<Note>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> GetAllNotes(CancellationToken cancellationToken)
    {
        var query = new GetAllNotesQuery();
        var result = await _mediator.Send(query, cancellationToken);

        if (result.IsFailure)
            return Problem(result.Error);

        return Ok(result.Value);
    }

}
