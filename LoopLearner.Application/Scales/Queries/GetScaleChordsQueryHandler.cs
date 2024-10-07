using CSharpFunctionalExtensions;
using LoopLearner.Application.Contracts.Persistence;
using LoopLearner.Domain.ScaleAggregate.ValueObjects;
using LoopLearner.Domain.SongAggregate.Entities;
using LoopLearner.Domain.SongAggregate.ValueObjects;

namespace LoopLearner.Application.Scales.Queries;

public class GetScaleChordsQueryHandler : IRequestHandler<GetScaleChordsQuery, Result<List<ChordDto>>>
{
    public async Task<Result<List<ChordDto>>> Handle(GetScaleChordsQuery request, CancellationToken cancellationToken)
    {
        return Result.Success(new List<ChordDto>());
        // Use the ChordBuilder to get chords dynamically
        var chords = ChordBuilder.GetChordsInKey(request.RootNote, request.ScaleType);
        
        if (chords == null || !chords.Any())
        {
            return Result.Failure<List<ChordDto>>("No chords found for the specified scale.");
        }

        return Result.Success(chords);
    }
}

public static class ChordBuilder
{
    private static readonly int[] MajorScaleIntervals = { 2, 2, 1, 2, 2, 2, 1 }; // W-W-H-W-W-W-H
    private static readonly int[] MinorScaleIntervals = { 2, 1, 2, 2, 1, 2, 2 }; // W-H-W-W-H-W-W

    public static List<ChordDto> GetChordsInKey(Note rootNote, ScaleType scaleType)
    {
        var scaleNotes = BuildScale(rootNote, scaleType);
        return BuildChordsFromScale(scaleNotes);
    }

    private static List<Note> BuildScale(Note rootNote, ScaleType scaleType)
    {
        var scale = new List<Note> { rootNote };
        int[] intervals = scaleType == ScaleType.Major ? MajorScaleIntervals : MinorScaleIntervals;

        foreach (var interval in intervals)
        {
            rootNote = GetNextNote(rootNote, interval);
            scale.Add(rootNote);
        }

        return scale;
    }

    private static Note GetNextNote(Note currentNote, int interval)
    {
        // Assume you have an ordered list of notes
        var allNotes = new List<Note>
        {
            Note.C, Note.CSharp, Note.D, Note.DSharp, Note.E,
            Note.F, Note.FSharp, Note.G, Note.GSharp, Note.A,
            Note.ASharp, Note.B
        };

        int currentIndex = allNotes.IndexOf(currentNote);
        int newIndex = (currentIndex + interval) % allNotes.Count;
    
        return allNotes[newIndex];
    }


    private static List<ChordDto> BuildChordsFromScale(List<Note> scaleNotes)
    {
        var chords = new List<ChordDto>();

        // Define the chord types for each degree in the scale (I, ii, iii, IV, V, vi, vii°)
        var chordTypes = new[]
        {
            ChordType.Major, // I
            ChordType.Minor, // ii
            ChordType.Minor, // iii
            ChordType.Major, // IV
            ChordType.Major, // V
            ChordType.Minor, // vi
            ChordType.Diminished // vii°
        };

        for (int i = 0; i < scaleNotes.Count; i++)
        {
            var root = scaleNotes[i];
            var chordType = chordTypes[i];

            // Create a chord DTO from the root note and its type
            var chordDto = new ChordDto(
                root.ToString(), // Convert Note to string if necessary
                chordType.ToString(), // Convert ChordType to string
                "", // Chord extension logic can be added here if necessary
                new List<string>() // Populate with actual FretNote positions if needed
            );
            chords.Add(chordDto);
        }

        return chords;
    }
}


public class ChordDto
{
    public string RootNote { get; set; }
    public string ChordType { get; set; }
    public string ChordExtension { get; set; }
    public List<string> Notes { get; set; }

    public ChordDto(string rootNote, string chordType, string chordExtension, List<string> notes)
    {
        RootNote = rootNote;
        ChordType = chordType;
        ChordExtension = chordExtension;
        Notes = notes;
    }
}

