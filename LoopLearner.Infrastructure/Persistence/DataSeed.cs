

using LoopLearner.Application.Contracts.Services;
using LoopLearner.Domain.SongAggregate.Entities;
using LoopLearner.Domain.SongAggregate.ValueObjects;
using LoopLearner.Domain.UserAggregate;
using LoopLearner.Domain.UserAggregate.ValueObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LoopLearner.Infrastructure.Persistence;

public static class DataSeed
{
    public static void Seed(ModelBuilder modelBuilder, IPasswordHasher<User> passwordHasher)
    {
        var alincoln = User.CreateNew("Abraham", "Lincoln", "alincoln", "lincoln.abraham@example.com", "HonestAbe1865");
        var jcaesar = User.CreateNew("Julius", "Caesar", "jcaesar", "caesar.julius@example.com", "EtTuBrute44BC");
        var aeinstein = User.CreateNew("Albert", "Einstein", "aeinstein", "einstein.albert@example.com", "E=mc2Genius");
        var mcurie = User.CreateNew("Marie", "Curie", "mcurie", "curie.marie@example.com", "Radioactive1898");
        var ldavinci = User.CreateNew("Leonardo", "da Vinci", "ldavinci", "davinci.leonardo@example.com",
            "Renaissance1452");
        var wshakespeare = User.CreateNew("William", "Shakespeare", "wshakespeare", "shakespeare.william@example.com",
            "ToBeOrNotToBe1600");
        var ccleopatra = User.CreateNew("Cleopatra", "", "ccleopatra", "cleopatra@example.com", "QueenOfEgypt30BC");
        var aalexander = User.CreateNew("Alexander", "the Great", "aalexander", "alexander@example.com",
            "Conqueror356BC");
        ;
        var ntesla = User.CreateNew("Nikola", "Tesla", "ntesla", "tesla.nikola@example.com", "ACPowerGenius1856");
        var wgenghis = User.CreateNew("Genghis", "Khan", "wgenghis", "genghis.khan@example.com", "MongolEmpire1162");

        var users = new[]
            { alincoln, jcaesar, aeinstein, mcurie, ldavinci, wshakespeare, ccleopatra, aalexander, ntesla, wgenghis };
        foreach (var user in users)
        {
            var hashedPassword = passwordHasher.HashPassword(user, user.Password);
            user.UpdatePassword(hashedPassword);
        }

        modelBuilder.Entity<User>().HasData(users);

    }

    public static async Task SeedNotes(LoopLearnerDbContext context)
    {
        var notes = new List<Note>
        {
            Note.CreateNew(NoteName.E, NotePosition.CreateNew(6, 0)),
            Note.CreateNew(NoteName.F, NotePosition.CreateNew(6, 1)),
            Note.CreateNew(NoteName.FSharp, NotePosition.CreateNew(6, 2)),
            Note.CreateNew(NoteName.G, NotePosition.CreateNew(6, 3)),
            Note.CreateNew(NoteName.GSharp, NotePosition.CreateNew(6, 4)),
            Note.CreateNew(NoteName.A, NotePosition.CreateNew(6, 5)),
            Note.CreateNew(NoteName.ASharp, NotePosition.CreateNew(6, 6)),
            Note.CreateNew(NoteName.B, NotePosition.CreateNew(6, 7)),
            Note.CreateNew(NoteName.C, NotePosition.CreateNew(6, 8)),
            Note.CreateNew(NoteName.CSharp, NotePosition.CreateNew(6, 9)),
            Note.CreateNew(NoteName.D, NotePosition.CreateNew(6, 10)),
            Note.CreateNew(NoteName.DSharp, NotePosition.CreateNew(6, 11)),
            Note.CreateNew(NoteName.E, NotePosition.CreateNew(6, 12)),
            Note.CreateNew(NoteName.F, NotePosition.CreateNew(6, 13)),
            Note.CreateNew(NoteName.FSharp, NotePosition.CreateNew(6, 14)),
            Note.CreateNew(NoteName.G, NotePosition.CreateNew(6, 15)),
            Note.CreateNew(NoteName.GSharp, NotePosition.CreateNew(6, 16)),
            Note.CreateNew(NoteName.A, NotePosition.CreateNew(6, 17)),
            Note.CreateNew(NoteName.ASharp, NotePosition.CreateNew(6, 18)),
            Note.CreateNew(NoteName.B, NotePosition.CreateNew(6, 19)),
            Note.CreateNew(NoteName.C, NotePosition.CreateNew(6, 20)),
            Note.CreateNew(NoteName.CSharp, NotePosition.CreateNew(6, 21)),
            Note.CreateNew(NoteName.D, NotePosition.CreateNew(6, 22)),
            Note.CreateNew(NoteName.DSharp, NotePosition.CreateNew(6, 23)),
            Note.CreateNew(NoteName.E, NotePosition.CreateNew(6, 24)),

            Note.CreateNew(NoteName.A, NotePosition.CreateNew(5, 0)),
            Note.CreateNew(NoteName.ASharp, NotePosition.CreateNew(5, 1)),
            Note.CreateNew(NoteName.B, NotePosition.CreateNew(5, 2)),
            Note.CreateNew(NoteName.C, NotePosition.CreateNew(5, 3)),
            Note.CreateNew(NoteName.CSharp, NotePosition.CreateNew(5, 4)),
            Note.CreateNew(NoteName.D, NotePosition.CreateNew(5, 5)),
            Note.CreateNew(NoteName.DSharp, NotePosition.CreateNew(5, 6)),
            Note.CreateNew(NoteName.E, NotePosition.CreateNew(5, 7)),
            Note.CreateNew(NoteName.F, NotePosition.CreateNew(5, 8)),
            Note.CreateNew(NoteName.FSharp, NotePosition.CreateNew(5, 9)),
            Note.CreateNew(NoteName.G, NotePosition.CreateNew(5, 10)),
            Note.CreateNew(NoteName.GSharp, NotePosition.CreateNew(5, 11)),
            Note.CreateNew(NoteName.A, NotePosition.CreateNew(5, 12)),
            Note.CreateNew(NoteName.ASharp, NotePosition.CreateNew(5, 13)),
            Note.CreateNew(NoteName.B, NotePosition.CreateNew(5, 14)),
            Note.CreateNew(NoteName.C, NotePosition.CreateNew(5, 15)),
            Note.CreateNew(NoteName.CSharp, NotePosition.CreateNew(5, 16)),
            Note.CreateNew(NoteName.D, NotePosition.CreateNew(5, 17)),
            Note.CreateNew(NoteName.DSharp, NotePosition.CreateNew(5, 18)),
            Note.CreateNew(NoteName.E, NotePosition.CreateNew(5, 19)),
            Note.CreateNew(NoteName.F, NotePosition.CreateNew(5, 20)),
            Note.CreateNew(NoteName.FSharp, NotePosition.CreateNew(5, 21)),
            Note.CreateNew(NoteName.G, NotePosition.CreateNew(5, 22)),
            Note.CreateNew(NoteName.GSharp, NotePosition.CreateNew(5, 23)),
            Note.CreateNew(NoteName.A, NotePosition.CreateNew(5, 24)),

            Note.CreateNew(NoteName.D, NotePosition.CreateNew(4, 0)),
            Note.CreateNew(NoteName.DSharp, NotePosition.CreateNew(4, 1)),
            Note.CreateNew(NoteName.E, NotePosition.CreateNew(4, 2)),
            Note.CreateNew(NoteName.F, NotePosition.CreateNew(4, 3)),
            Note.CreateNew(NoteName.FSharp, NotePosition.CreateNew(4, 4)),
            Note.CreateNew(NoteName.G, NotePosition.CreateNew(4, 5)),
            Note.CreateNew(NoteName.GSharp, NotePosition.CreateNew(4, 6)),
            Note.CreateNew(NoteName.A, NotePosition.CreateNew(4, 7)),
            Note.CreateNew(NoteName.ASharp, NotePosition.CreateNew(4, 8)),
            Note.CreateNew(NoteName.B, NotePosition.CreateNew(4, 9)),
            Note.CreateNew(NoteName.C, NotePosition.CreateNew(4, 10)),
            Note.CreateNew(NoteName.CSharp, NotePosition.CreateNew(4, 11)),
            Note.CreateNew(NoteName.D, NotePosition.CreateNew(4, 12)),
            Note.CreateNew(NoteName.DSharp, NotePosition.CreateNew(4, 13)),
            Note.CreateNew(NoteName.E, NotePosition.CreateNew(4, 14)),
            Note.CreateNew(NoteName.F, NotePosition.CreateNew(4, 15)),
            Note.CreateNew(NoteName.FSharp, NotePosition.CreateNew(4, 16)),
            Note.CreateNew(NoteName.G, NotePosition.CreateNew(4, 17)),
            Note.CreateNew(NoteName.GSharp, NotePosition.CreateNew(4, 18)),
            Note.CreateNew(NoteName.A, NotePosition.CreateNew(4, 19)),
            Note.CreateNew(NoteName.ASharp, NotePosition.CreateNew(4, 20)),
            Note.CreateNew(NoteName.B, NotePosition.CreateNew(4, 21)),
            Note.CreateNew(NoteName.C, NotePosition.CreateNew(4, 22)),
            Note.CreateNew(NoteName.CSharp, NotePosition.CreateNew(4, 23)),
            Note.CreateNew(NoteName.D, NotePosition.CreateNew(4, 24)),

            Note.CreateNew(NoteName.G, NotePosition.CreateNew(3, 0)),
            Note.CreateNew(NoteName.GSharp, NotePosition.CreateNew(3, 1)),
            Note.CreateNew(NoteName.A, NotePosition.CreateNew(3, 2)),
            Note.CreateNew(NoteName.ASharp, NotePosition.CreateNew(3, 3)),
            Note.CreateNew(NoteName.B, NotePosition.CreateNew(3, 4)),
            Note.CreateNew(NoteName.C, NotePosition.CreateNew(3, 5)),
            Note.CreateNew(NoteName.CSharp, NotePosition.CreateNew(3, 6)),
            Note.CreateNew(NoteName.D, NotePosition.CreateNew(3, 7)),
            Note.CreateNew(NoteName.DSharp, NotePosition.CreateNew(3, 8)),
            Note.CreateNew(NoteName.E, NotePosition.CreateNew(3, 9)),
            Note.CreateNew(NoteName.F, NotePosition.CreateNew(3, 10)),
            Note.CreateNew(NoteName.FSharp, NotePosition.CreateNew(3, 11)),
            Note.CreateNew(NoteName.G, NotePosition.CreateNew(3, 12)),
            Note.CreateNew(NoteName.GSharp, NotePosition.CreateNew(3, 13)),
            Note.CreateNew(NoteName.A, NotePosition.CreateNew(3, 14)),
            Note.CreateNew(NoteName.ASharp, NotePosition.CreateNew(3, 15)),
            Note.CreateNew(NoteName.B, NotePosition.CreateNew(3, 16)),
            Note.CreateNew(NoteName.C, NotePosition.CreateNew(3, 17)),
            Note.CreateNew(NoteName.CSharp, NotePosition.CreateNew(3, 18)),
            Note.CreateNew(NoteName.D, NotePosition.CreateNew(3, 19)),
            Note.CreateNew(NoteName.DSharp, NotePosition.CreateNew(3, 20)),
            Note.CreateNew(NoteName.E, NotePosition.CreateNew(3, 21)),
            Note.CreateNew(NoteName.F, NotePosition.CreateNew(3, 22)),
            Note.CreateNew(NoteName.FSharp, NotePosition.CreateNew(3, 23)),
            Note.CreateNew(NoteName.G, NotePosition.CreateNew(3, 24)),

            Note.CreateNew(NoteName.B, NotePosition.CreateNew(2, 0)),
            Note.CreateNew(NoteName.C, NotePosition.CreateNew(2, 1)),
            Note.CreateNew(NoteName.CSharp, NotePosition.CreateNew(2, 2)),
            Note.CreateNew(NoteName.D, NotePosition.CreateNew(2, 3)),
            Note.CreateNew(NoteName.DSharp, NotePosition.CreateNew(2, 4)),
            Note.CreateNew(NoteName.E, NotePosition.CreateNew(2, 5)),
            Note.CreateNew(NoteName.F, NotePosition.CreateNew(2, 6)),
            Note.CreateNew(NoteName.FSharp, NotePosition.CreateNew(2, 7)),
            Note.CreateNew(NoteName.G, NotePosition.CreateNew(2, 8)),
            Note.CreateNew(NoteName.GSharp, NotePosition.CreateNew(2, 9)),
            Note.CreateNew(NoteName.A, NotePosition.CreateNew(2, 10)),
            Note.CreateNew(NoteName.ASharp, NotePosition.CreateNew(2, 11)),
            Note.CreateNew(NoteName.B, NotePosition.CreateNew(2, 12)),
            Note.CreateNew(NoteName.C, NotePosition.CreateNew(2, 13)),
            Note.CreateNew(NoteName.CSharp, NotePosition.CreateNew(2, 14)),
            Note.CreateNew(NoteName.D, NotePosition.CreateNew(2, 15)),
            Note.CreateNew(NoteName.DSharp, NotePosition.CreateNew(2, 16)),
            Note.CreateNew(NoteName.E, NotePosition.CreateNew(2, 17)),
            Note.CreateNew(NoteName.F, NotePosition.CreateNew(2, 18)),
            Note.CreateNew(NoteName.FSharp, NotePosition.CreateNew(2, 19)),
            Note.CreateNew(NoteName.G, NotePosition.CreateNew(2, 20)),
            Note.CreateNew(NoteName.GSharp, NotePosition.CreateNew(2, 21)),
            Note.CreateNew(NoteName.A, NotePosition.CreateNew(2, 22)),
            Note.CreateNew(NoteName.ASharp, NotePosition.CreateNew(2, 23)),
            Note.CreateNew(NoteName.B, NotePosition.CreateNew(2, 24)),

            Note.CreateNew(NoteName.E, NotePosition.CreateNew(1, 0)),
            Note.CreateNew(NoteName.F, NotePosition.CreateNew(1, 1)),
            Note.CreateNew(NoteName.FSharp, NotePosition.CreateNew(1, 2)),
            Note.CreateNew(NoteName.G, NotePosition.CreateNew(1, 3)),
            Note.CreateNew(NoteName.GSharp, NotePosition.CreateNew(1, 4)),
            Note.CreateNew(NoteName.A, NotePosition.CreateNew(1, 5)),
            Note.CreateNew(NoteName.ASharp, NotePosition.CreateNew(1, 6)),
            Note.CreateNew(NoteName.B, NotePosition.CreateNew(1, 7)),
            Note.CreateNew(NoteName.C, NotePosition.CreateNew(1, 8)),
            Note.CreateNew(NoteName.CSharp, NotePosition.CreateNew(1, 9)),
            Note.CreateNew(NoteName.D, NotePosition.CreateNew(1, 10)),
            Note.CreateNew(NoteName.DSharp, NotePosition.CreateNew(1, 11)),
            Note.CreateNew(NoteName.E, NotePosition.CreateNew(1, 12)),
            Note.CreateNew(NoteName.F, NotePosition.CreateNew(1, 13)),
            Note.CreateNew(NoteName.FSharp, NotePosition.CreateNew(1, 14)),
            Note.CreateNew(NoteName.G, NotePosition.CreateNew(1, 15)),
            Note.CreateNew(NoteName.GSharp, NotePosition.CreateNew(1, 16)),
            Note.CreateNew(NoteName.A, NotePosition.CreateNew(1, 17)),
            Note.CreateNew(NoteName.ASharp, NotePosition.CreateNew(1, 18)),
            Note.CreateNew(NoteName.B, NotePosition.CreateNew(1, 19)),
            Note.CreateNew(NoteName.C, NotePosition.CreateNew(1, 20)),
            Note.CreateNew(NoteName.CSharp, NotePosition.CreateNew(1, 21)),
            Note.CreateNew(NoteName.D, NotePosition.CreateNew(1, 22)),
            Note.CreateNew(NoteName.DSharp, NotePosition.CreateNew(1, 23)),
            Note.CreateNew(NoteName.E, NotePosition.CreateNew(1, 24))
        };

        await context.Notes.AddRangeAsync(notes);
        await context.SaveChangesAsync();
    }


    public static void SeedNotePositions(ModelBuilder modelBuilder)
    {
        var notePositions = new List<NotePosition>
        {
            NotePosition.CreateNew(6, 0),
            NotePosition.CreateNew(6, 1),
            NotePosition.CreateNew(6, 2),
            NotePosition.CreateNew(6, 3),
            NotePosition.CreateNew(6, 4),
            NotePosition.CreateNew(6, 5),
            NotePosition.CreateNew(6, 6),
            NotePosition.CreateNew(6, 7),
            NotePosition.CreateNew(6, 8),
            NotePosition.CreateNew(6, 9),
            NotePosition.CreateNew(6, 10),
            NotePosition.CreateNew(6, 11),
            NotePosition.CreateNew(6, 12),
            NotePosition.CreateNew(6, 13),
            NotePosition.CreateNew(6, 14),
            NotePosition.CreateNew(6, 15),
            NotePosition.CreateNew(6, 16),
            NotePosition.CreateNew(6, 17),
            NotePosition.CreateNew(6, 18),
            NotePosition.CreateNew(6, 19),
            NotePosition.CreateNew(6, 20),
            NotePosition.CreateNew(6, 21),
            NotePosition.CreateNew(6, 22),
            NotePosition.CreateNew(6, 23),
            NotePosition.CreateNew(6, 24),

            NotePosition.CreateNew(5, 0),
            NotePosition.CreateNew(5, 1),
            NotePosition.CreateNew(5, 2),
            NotePosition.CreateNew(5, 3),
            NotePosition.CreateNew(5, 4),
            NotePosition.CreateNew(5, 5),
            NotePosition.CreateNew(5, 6),
            NotePosition.CreateNew(5, 7),
            NotePosition.CreateNew(5, 8),
            NotePosition.CreateNew(5, 9),
            NotePosition.CreateNew(5, 10),
            NotePosition.CreateNew(5, 11),
            NotePosition.CreateNew(5, 12),
            NotePosition.CreateNew(5, 13),
            NotePosition.CreateNew(5, 14),
            NotePosition.CreateNew(5, 15),
            NotePosition.CreateNew(5, 16),
            NotePosition.CreateNew(5, 17),
            NotePosition.CreateNew(5, 18),
            NotePosition.CreateNew(5, 19),
            NotePosition.CreateNew(5, 20),
            NotePosition.CreateNew(5, 21),
            NotePosition.CreateNew(5, 22),
            NotePosition.CreateNew(5, 23),
            NotePosition.CreateNew(5, 24),

            NotePosition.CreateNew(4, 0),
            NotePosition.CreateNew(4, 1),
            NotePosition.CreateNew(4, 2),
            NotePosition.CreateNew(4, 3),
            NotePosition.CreateNew(4, 4),
            NotePosition.CreateNew(4, 5),
            NotePosition.CreateNew(4, 6),
            NotePosition.CreateNew(4, 7),
            NotePosition.CreateNew(4, 8),
            NotePosition.CreateNew(4, 9),
            NotePosition.CreateNew(4, 10),
            NotePosition.CreateNew(4, 11),
            NotePosition.CreateNew(4, 12),
            NotePosition.CreateNew(4, 13),
            NotePosition.CreateNew(4, 14),
            NotePosition.CreateNew(4, 15),
            NotePosition.CreateNew(4, 16),
            NotePosition.CreateNew(4, 17),
            NotePosition.CreateNew(4, 18),
            NotePosition.CreateNew(4, 19),
            NotePosition.CreateNew(4, 20),
            NotePosition.CreateNew(4, 21),
            NotePosition.CreateNew(4, 22),
            NotePosition.CreateNew(4, 23),
            NotePosition.CreateNew(4, 24),

            NotePosition.CreateNew(3, 0),
            NotePosition.CreateNew(3, 1),
            NotePosition.CreateNew(3, 2),
            NotePosition.CreateNew(3, 3),
            NotePosition.CreateNew(3, 4),
            NotePosition.CreateNew(3, 5),
            NotePosition.CreateNew(3, 6),
            NotePosition.CreateNew(3, 7),
            NotePosition.CreateNew(3, 8),
            NotePosition.CreateNew(3, 9),
            NotePosition.CreateNew(3, 10),
            NotePosition.CreateNew(3, 11),
            NotePosition.CreateNew(3, 12),
            NotePosition.CreateNew(3, 13),
            NotePosition.CreateNew(3, 14),
            NotePosition.CreateNew(3, 15),
            NotePosition.CreateNew(3, 16),
            NotePosition.CreateNew(3, 17),
            NotePosition.CreateNew(3, 18),
            NotePosition.CreateNew(3, 19),
            NotePosition.CreateNew(3, 20),
            NotePosition.CreateNew(3, 21),
            NotePosition.CreateNew(3, 22),
            NotePosition.CreateNew(3, 23),
            NotePosition.CreateNew(3, 24),

            NotePosition.CreateNew(2, 0),
            NotePosition.CreateNew(2, 1),
            NotePosition.CreateNew(2, 2),
            NotePosition.CreateNew(2, 3),
            NotePosition.CreateNew(2, 4),
            NotePosition.CreateNew(2, 5),
            NotePosition.CreateNew(2, 6),
            NotePosition.CreateNew(2, 7),
            NotePosition.CreateNew(2, 8),
            NotePosition.CreateNew(2, 9),
            NotePosition.CreateNew(2, 10),
            NotePosition.CreateNew(2, 11),
            NotePosition.CreateNew(2, 12),
            NotePosition.CreateNew(2, 13),
            NotePosition.CreateNew(2, 14),
            NotePosition.CreateNew(2, 15),
            NotePosition.CreateNew(2, 16),
            NotePosition.CreateNew(2, 17),
            NotePosition.CreateNew(2, 18),
            NotePosition.CreateNew(2, 19),
            NotePosition.CreateNew(2, 20),
            NotePosition.CreateNew(2, 21),
            NotePosition.CreateNew(2, 22),
            NotePosition.CreateNew(2, 23),
            NotePosition.CreateNew(2, 24),

            NotePosition.CreateNew(1, 0),
            NotePosition.CreateNew(1, 1),
            NotePosition.CreateNew(1, 2),
            NotePosition.CreateNew(1, 3),
            NotePosition.CreateNew(1, 4),
            NotePosition.CreateNew(1, 5),
            NotePosition.CreateNew(1, 6),
            NotePosition.CreateNew(1, 7),
            NotePosition.CreateNew(1, 8),
            NotePosition.CreateNew(1, 9),
            NotePosition.CreateNew(1, 10),
            NotePosition.CreateNew(1, 11),
            NotePosition.CreateNew(1, 12),
            NotePosition.CreateNew(1, 13),
            NotePosition.CreateNew(1, 14),
            NotePosition.CreateNew(1, 15),
            NotePosition.CreateNew(1, 16),
            NotePosition.CreateNew(1, 17),
            NotePosition.CreateNew(1, 18),
            NotePosition.CreateNew(1, 19),
            NotePosition.CreateNew(1, 20),
            NotePosition.CreateNew(1, 21),
            NotePosition.CreateNew(1, 22),
            NotePosition.CreateNew(1, 23),
            NotePosition.CreateNew(1, 24),
        };

        modelBuilder.Entity<NotePosition>().HasData(notePositions);
    }


    public static async Task SeedStandardTuningOpenChords(LoopLearnerDbContext context)
{
    // Fetch Note objects for all open chords
    var cNote = await context.Notes.FirstOrDefaultAsync(n => n.NoteName == NoteName.C && n.Position.StringNumber == 5 && n.Position.FretNumber == 3);
    var eNote = await context.Notes.FirstOrDefaultAsync(n => n.NoteName == NoteName.E && n.Position.StringNumber == 4 && n.Position.FretNumber == 2);
    var gOpenNote = await context.Notes.FirstOrDefaultAsync(n => n.NoteName == NoteName.G && n.Position.StringNumber == 3 && n.Position.FretNumber == 0);
    var bOpenNote = await context.Notes.FirstOrDefaultAsync(n => n.NoteName == NoteName.B && n.Position.StringNumber == 2 && n.Position.FretNumber == 0);
    var highEOpenNote = await context.Notes.FirstOrDefaultAsync(n => n.NoteName == NoteName.E && n.Position.StringNumber == 1 && n.Position.FretNumber == 0);

    var dOpenNote = await context.Notes.FirstOrDefaultAsync(n => n.NoteName == NoteName.D && n.Position.StringNumber == 4 && n.Position.FretNumber == 0);
    var fSharpNote = await context.Notes.FirstOrDefaultAsync(n => n.NoteName == NoteName.FSharp && n.Position.StringNumber == 1 && n.Position.FretNumber == 2);
    var aOpenNote = await context.Notes.FirstOrDefaultAsync(n => n.NoteName == NoteName.A && n.Position.StringNumber == 5 && n.Position.FretNumber == 0);

    var fNote = await context.Notes.FirstOrDefaultAsync(n => n.NoteName == NoteName.F && n.Position.StringNumber == 1 && n.Position.FretNumber == 1);

    var lowEOpenNote = await context.Notes.FirstOrDefaultAsync(n => n.NoteName == NoteName.E && n.Position.StringNumber == 6 && n.Position.FretNumber == 0);
    var gSharpNote = await context.Notes.FirstOrDefaultAsync(n => n.NoteName == NoteName.GSharp && n.Position.StringNumber == 3 && n.Position.FretNumber == 1);

    // Create and seed the open chords with full Note objects
    var chords = new List<Chord>
    {
        Chord.CreateNew(NoteName.C, ChordType.Major, ChordExtension.None, new List<Note> { cNote, eNote, gOpenNote, bOpenNote, highEOpenNote }),
        Chord.CreateNew(NoteName.D, ChordType.Major, ChordExtension.None, new List<Note> { dOpenNote, fSharpNote, aOpenNote }),
        Chord.CreateNew(NoteName.D, ChordType.Minor, ChordExtension.None, new List<Note> { dOpenNote, fNote, aOpenNote }),
        Chord.CreateNew(NoteName.E, ChordType.Major, ChordExtension.None, new List<Note> { lowEOpenNote, gSharpNote, bOpenNote, highEOpenNote }),
        Chord.CreateNew(NoteName.E, ChordType.Minor, ChordExtension.None, new List<Note> { lowEOpenNote, gOpenNote, bOpenNote, highEOpenNote }),
        Chord.CreateNew(NoteName.G, ChordType.Major, ChordExtension.None, new List<Note> { lowEOpenNote, dOpenNote, bOpenNote, highEOpenNote }),
        Chord.CreateNew(NoteName.A, ChordType.Major, ChordExtension.None, new List<Note> { aOpenNote, fSharpNote, highEOpenNote }),
        Chord.CreateNew(NoteName.A, ChordType.Minor, ChordExtension.None, new List<Note> { aOpenNote, cNote, eNote })
    };

    await context.Chords.AddRangeAsync(chords);
    await context.SaveChangesAsync();
}

}



