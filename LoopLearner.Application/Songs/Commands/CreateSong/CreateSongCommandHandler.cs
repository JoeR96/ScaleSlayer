using CSharpFunctionalExtensions;
using LoopLearner.Application.Contracts.Persistence;
using LoopLearner.Application.Extensions;
using LoopLearner.Application.Songs.Commands.SharedLogic;
using LoopLearner.Domain.Errors;
using LoopLearner.Domain.Errors.General;
using LoopLearner.Domain.SongAggregate;
using LoopLearner.Domain.SongAggregate.Entities;

namespace LoopLearner.Application.Songs.CreateSong;

public class CreateSongCommandHandler : IRequestHandler<CreateSongCommand, Result<Song, Error>>
{
    private readonly ISongRepository _songRepository;
    private readonly IInstrumentPartRepository _instrumentPartRepository;

    public CreateSongCommandHandler(ISongRepository songRepository, IInstrumentPartRepository instrumentPartRepository)
    {
        _songRepository = songRepository ?? throw new ArgumentNullException(nameof(songRepository));
        _instrumentPartRepository = instrumentPartRepository ?? throw new ArgumentNullException(nameof(instrumentPartRepository));
    }
    
    public async Task<Result<Song, Error>> Handle(CreateSongCommand request, CancellationToken cancellationToken)
    {
        Result<bool, ValidationError> validationResult = await request.ValidateAsync(new CreateSongCommandValidator(), cancellationToken);
        if (validationResult.IsFailure)
            return validationResult.Error;
        
        var result = await InstrumentPartHelpers.ValidateCreateAndGetInstrumentPartsAsync(request.InstrumentParts.ToList(), _instrumentPartRepository);
        if (result.IsFailure)
            return result.Error;

        List<InstrumentPart> instrumentParts = result.Value;

        var song = Song.CreateNew(request.Title, request.Artist, request.Genre, request.BPM);

        instrumentParts.ForEach(ip => song.AddInstrumentPart(ip));

        _songRepository.AddSong(song);

        await _songRepository.SaveChangesAsync();

        return song;    }
}