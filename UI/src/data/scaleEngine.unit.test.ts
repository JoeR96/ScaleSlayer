import { describe, it, expect } from 'vitest';
import { getScaleNoteNames } from './scaleEngine';
import { Note, Scale } from '../enums/enums';

describe('ScaleEngine - Unit Tests', () => {
    describe('getScaleNoteNames with Pentatonic Minor Scale', () => {
        const testCases: Array<{ rootNote: Note; expectedNotes: Note[] }> = [
            { rootNote: Note.C, expectedNotes: [Note.C, Note.DSharp, Note.F, Note.G, Note.ASharp] },
            { rootNote: Note.CSharp, expectedNotes: [Note.CSharp, Note.E, Note.FSharp, Note.GSharp, Note.B] },
            { rootNote: Note.D, expectedNotes: [Note.D, Note.F, Note.G, Note.A, Note.C] },
            { rootNote: Note.DSharp, expectedNotes: [Note.DSharp, Note.FSharp, Note.GSharp, Note.ASharp, Note.CSharp] },
            { rootNote: Note.E, expectedNotes: [Note.E, Note.G, Note.A, Note.B, Note.D] },
            { rootNote: Note.F, expectedNotes: [Note.F, Note.GSharp, Note.ASharp, Note.C, Note.DSharp] },
            { rootNote: Note.FSharp, expectedNotes: [Note.FSharp, Note.A, Note.B, Note.CSharp, Note.E] },
            { rootNote: Note.G, expectedNotes: [Note.G, Note.ASharp, Note.C, Note.D, Note.F] },
            { rootNote: Note.GSharp, expectedNotes: [Note.GSharp, Note.B, Note.CSharp, Note.DSharp, Note.FSharp] },
            { rootNote: Note.A, expectedNotes: [Note.A, Note.C, Note.D, Note.E, Note.G] },
            { rootNote: Note.ASharp, expectedNotes: [Note.ASharp, Note.CSharp, Note.DSharp, Note.F, Note.GSharp] },
            { rootNote: Note.B, expectedNotes: [Note.B, Note.D, Note.E, Note.FSharp, Note.A] },
        ];

        testCases.forEach(({ rootNote, expectedNotes }) => {
            it(`should return correct notes for ${rootNote} Pentatonic Minor`, () => {
                const result = getScaleNoteNames(rootNote, Scale.PentatonicMinor);

                expect(result).toHaveLength(5);
                expect(result).toEqual(expectedNotes);
            });
        });
    });
});
