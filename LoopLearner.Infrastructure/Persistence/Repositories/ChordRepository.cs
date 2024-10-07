using LoopLearner.Application.Contracts.Persistence;
using LoopLearner.Domain.ScaleAggregate.ValueObjects;
using LoopLearner.Domain.SongAggregate.Entities;
using LoopLearner.Domain.SongAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace LoopLearner.Infrastructure.Persistence.Repositories;

public class ChordRepository(LoopLearnerDbContext context) : IChordRepository
{
    public async Task<IEnumerable<Chord>> GetChordCollectionAsync(IEnumerable<Guid> chordIds, CancellationToken cancellationToken)
    {
        return await context.Chords
            .Include(c => c.Notes)
            .ThenInclude(n => n.Position)
            .Where(c => chordIds.Contains(c.Id.Value))
            .ToListAsync(cancellationToken);
    }
    
public async Task<List<Chord>> GetChordsForScaleAsync(Note rootNote, ScaleType scaleType, CancellationToken cancellationToken)
{
    // Return only the specific C# minor scale chords
    if (rootNote == Note.CSharp && scaleType == ScaleType.Minor)
    {
        // Hardcoded chord names and types for the C# minor scale
        var cSharpMinorChord = await context.Chords
            .Include(c => c.Notes)
            .ThenInclude(n => n.Position)
            .FirstOrDefaultAsync(c => c.RootNote == Note.CSharp && c.ChordType == ChordType.Minor && c.ChordExtension == ChordExtension.None, cancellationToken);

        var eMajorChord = await context.Chords
            .Include(c => c.Notes)
            .ThenInclude(n => n.Position)
            .FirstOrDefaultAsync(c => c.RootNote == Note.E && c.ChordType == ChordType.Major && c.ChordExtension == ChordExtension.None, cancellationToken);

        var fSharpMinorChord = await context.Chords
            .Include(c => c.Notes)
            .ThenInclude(n => n.Position)
            .FirstOrDefaultAsync(c => c.RootNote == Note.FSharp && c.ChordType == ChordType.Minor && c.ChordExtension == ChordExtension.None, cancellationToken);

        var gSharpMinorChord = await context.Chords
            .Include(c => c.Notes)
            .ThenInclude(n => n.Position)
            .FirstOrDefaultAsync(c => c.RootNote == Note.GSharp && c.ChordType == ChordType.Minor && c.ChordExtension == ChordExtension.None, cancellationToken);

        var aMajorChord = await context.Chords
            .Include(c => c.Notes)
            .ThenInclude(n => n.Position)
            .FirstOrDefaultAsync(c => c.RootNote == Note.A && c.ChordType == ChordType.Major && c.ChordExtension == ChordExtension.None, cancellationToken);

        var bMajorChord = await context.Chords
            .Include(c => c.Notes)
            .ThenInclude(n => n.Position)
            .FirstOrDefaultAsync(c => c.RootNote == Note.B && c.ChordType == ChordType.Major && c.ChordExtension == ChordExtension.None, cancellationToken);

        // Variations
        var cSharpMinor7Chord = await context.Chords
            .Include(c => c.Notes)
            .ThenInclude(n => n.Position)
            .FirstOrDefaultAsync(c => c.RootNote == Note.CSharp && c.ChordType == ChordType.Minor && c.ChordExtension == ChordExtension.Seventh, cancellationToken);

        var eMajor7Chord = await context.Chords
            .Include(c => c.Notes)
            .ThenInclude(n => n.Position)
            .FirstOrDefaultAsync(c => c.RootNote == Note.E && c.ChordType == ChordType.Major && c.ChordExtension == ChordExtension.Seventh, cancellationToken);

        var fSharpMinor7Chord = await context.Chords
            .Include(c => c.Notes)
            .ThenInclude(n => n.Position)
            .FirstOrDefaultAsync(c => c.RootNote == Note.FSharp && c.ChordType == ChordType.Minor && c.ChordExtension == ChordExtension.Seventh, cancellationToken);

        var gSharpMinor7Chord = await context.Chords
            .Include(c => c.Notes)
            .ThenInclude(n => n.Position)
            .FirstOrDefaultAsync(c => c.RootNote == Note.GSharp && c.ChordType == ChordType.Minor && c.ChordExtension == ChordExtension.Seventh, cancellationToken);

        var aMajor9Chord = await context.Chords
            .Include(c => c.Notes)
            .ThenInclude(n => n.Position)
            .FirstOrDefaultAsync(c => c.RootNote == Note.A && c.ChordType == ChordType.Major && c.ChordExtension == ChordExtension.Ninth, cancellationToken);

        // Collect all the chords into a list and return
        return new List<Chord>
        {
            cSharpMinorChord,
            eMajorChord,
            fSharpMinorChord,
            gSharpMinorChord,
            aMajorChord,
            bMajorChord,
            cSharpMinor7Chord,
            eMajor7Chord,
            fSharpMinor7Chord,
            gSharpMinor7Chord,
            aMajor9Chord
        };
    }

    // Return an empty list for other scales
    return new List<Chord>();
}

}