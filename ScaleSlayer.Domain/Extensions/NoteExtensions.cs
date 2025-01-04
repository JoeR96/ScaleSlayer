using ScaleSlayer.Domain.ScaleAggregate.ValueObjects;

namespace ScaleSlayer.Domain.Extensions;

public static class NoteExtensions
{
    private static readonly Note[] ChromaticScale =
    {
        Note.E, Note.F, Note.FSharp, Note.G, Note.GSharp, Note.A, Note.ASharp, Note.B,
        Note.C, Note.CSharp, Note.D, Note.DSharp
    };

    public static Note[] GetChromaticScale(this Note rootNote) => ChromaticScale;

    public static int GetRootNoteOffset(this Note rootNote)
    {
        int index = Array.IndexOf(ChromaticScale, rootNote);
        if (index == -1)
            throw new ArgumentException($"Invalid root note: {rootNote}");
        return index;
    }
}
