using FluentAssertions;
using ScaleSlayer.Domain.ScaleAggregate.ValueObjects;
using ScaleSlayer.UnitTests.Helpers;

namespace ScaleSlayer.UnitTests.Tests;

[TestFixture]
public class ScaleTests
{
    [TestCase(Note.C, new[] { Note.C, Note.DSharp, Note.F, Note.G, Note.ASharp })]
    [TestCase(Note.CSharp, new[] { Note.CSharp, Note.E, Note.FSharp, Note.GSharp, Note.B })]
    [TestCase(Note.D, new[] { Note.D, Note.F, Note.G, Note.A, Note.C })]
    [TestCase(Note.DSharp, new[] { Note.DSharp, Note.FSharp, Note.GSharp, Note.ASharp, Note.CSharp })]
    [TestCase(Note.E, new[] { Note.E, Note.G, Note.A, Note.B, Note.D })]
    [TestCase(Note.F, new[] { Note.F, Note.GSharp, Note.ASharp, Note.C, Note.DSharp })]
    [TestCase(Note.FSharp, new[] { Note.FSharp, Note.A, Note.B, Note.CSharp, Note.E })]
    [TestCase(Note.G, new[] { Note.G, Note.ASharp, Note.C, Note.D, Note.F})]
    [TestCase(Note.GSharp, new[] { Note.GSharp, Note.B, Note.CSharp, Note.DSharp, Note.FSharp })]
    [TestCase(Note.A, new[] { Note.A, Note.C, Note.D, Note.E, Note.G })]
    [TestCase(Note.ASharp, new[] { Note.ASharp, Note.CSharp, Note.DSharp, Note.F, Note.GSharp })]
    [TestCase(Note.B, new[] { Note.B, Note.D, Note.E, Note.FSharp, Note.A })]
    public void GetScaleNoteNames_WithPentatonicMinorScale_ReturnsCorrectNotes(Note rootNote, Note[] expectedNotes)
    {
        var scale = ScaleHelper.CreateScale(rootNote, ScaleType.PentatonicMinor);

        var result = scale.GetScaleNoteNames();

        result.Should().HaveCount(5);
        result.Should().Equal(expectedNotes);
    }

}