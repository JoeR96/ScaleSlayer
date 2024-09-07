using CSharpFunctionalExtensions;
using LoopLearner.Domain.Errors;
using LoopLearner.Domain.SongAggregate;
using LoopLearner.Domain.SongAggregate.Entities;

namespace LoopLearner.Application.Songs.CreateSong;

public record CreateSongCommand(string Title, string Artist, string Genre, int BPM, IEnumerable<InstrumentPartCommand> InstrumentParts) : IRequest<Result<Song, Error>>;
public record InstrumentPartCommand(Guid? Id,string InstrumentName, IEnumerable<Tab> Tabs);

