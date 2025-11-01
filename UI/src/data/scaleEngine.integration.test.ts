import { describe, it, expect } from 'vitest';
import { getScaleNotes } from './scaleEngine';
import { Note, Scale } from '../enums/enums';

describe('ScaleEngine - Integration Tests', () => {
    describe('Box grouping for Pentatonic Minor scales', () => {
        // Test data from C# integration tests
        const testCases: Array<{
            rootNote: Note;
            allowedFretNumbersPerBox: Record<string, number[]>;
        }> = [
            {
                rootNote: Note.A,
                allowedFretNumbersPerBox: {
                    'Box1': [5, 7, 8, 17, 19, 20],
                    'Box2': [7, 8, 9, 10, 19, 20, 21, 22],
                    'Box3': [9, 10, 12, 13, 21, 22],
                    'Box4': [0, 1, 2, 3, 12, 13, 14, 15],
                    'Box5': [2, 3, 5, 14, 15, 17],
                },
            },
            {
                rootNote: Note.ASharp,
                allowedFretNumbersPerBox: {
                    'Box1': [6, 8, 9, 18, 20, 21],
                    'Box2': [8, 9, 10, 11, 20, 21, 22],
                    'Box3': [10, 11, 13, 14],
                    'Box4': [1, 2, 3, 4, 13, 14, 15, 16],
                    'Box5': [3, 4, 6, 15, 16, 18],
                },
            },
            {
                rootNote: Note.B,
                allowedFretNumbersPerBox: {
                    'Box1': [7, 9, 10, 19, 21, 22],
                    'Box2': [9, 10, 11, 12, 21, 22],
                    'Box3': [11, 12, 14, 15],
                    'Box4': [2, 3, 4, 5, 14, 15, 16, 17],
                    'Box5': [4, 5, 7, 16, 17, 19],
                },
            },
            {
                rootNote: Note.C,
                allowedFretNumbersPerBox: {
                    'Box1': [8, 10, 11, 20, 22],
                    'Box2': [10, 11, 12, 13],
                    'Box3': [0, 1, 3, 4, 12, 13, 15, 16],
                    'Box4': [3, 4, 5, 6, 15, 16, 17, 18],
                    'Box5': [5, 6, 8, 17, 18, 20],
                },
            },
            {
                rootNote: Note.CSharp,
                allowedFretNumbersPerBox: {
                    'Box1': [9, 11, 12, 21],
                    'Box2': [11, 12, 13, 14],
                    'Box3': [1, 2, 4, 5, 13, 14, 16, 17],
                    'Box4': [4, 5, 6, 7, 16, 17, 18, 19],
                    'Box5': [6, 7, 9, 18, 19, 21],
                },
            },
            {
                rootNote: Note.D,
                allowedFretNumbersPerBox: {
                    'Box1': [10, 12, 13],
                    'Box2': [0, 1, 2, 3, 12, 13, 14, 15],
                    'Box3': [2, 3, 5, 6, 14, 15, 17, 18],
                    'Box4': [5, 6, 7, 8, 17, 18, 19, 20],
                    'Box5': [7, 8, 10, 19, 20, 22],
                },
            },
            {
                rootNote: Note.DSharp,
                allowedFretNumbersPerBox: {
                    'Box1': [11, 13, 14],
                    'Box2': [1, 2, 3, 4, 13, 14, 15, 16],
                    'Box3': [3, 4, 6, 7, 15, 16, 18, 19],
                    'Box4': [6, 7, 8, 9, 18, 19, 20, 21],
                    'Box5': [8, 9, 11, 20, 21],
                },
            },
            {
                rootNote: Note.E,
                allowedFretNumbersPerBox: {
                    'Box1': [0, 2, 3, 12, 14, 15],
                    'Box2': [2, 3, 4, 5, 14, 15, 16, 17],
                    'Box3': [4, 5, 7, 8, 16, 17, 19, 20],
                    'Box4': [7, 8, 9, 10, 19, 20, 21, 22],
                    'Box5': [9, 10, 12, 21, 22],
                },
            },
            {
                rootNote: Note.F,
                allowedFretNumbersPerBox: {
                    'Box1': [1, 3, 4, 13, 15, 16],
                    'Box2': [3, 4, 5, 6, 15, 16, 17, 18],
                    'Box3': [5, 6, 8, 9, 17, 18, 20, 21],
                    'Box4': [8, 9, 10, 11, 20, 21, 22],
                    'Box5': [10, 11, 13],
                },
            },
            {
                rootNote: Note.FSharp,
                allowedFretNumbersPerBox: {
                    'Box1': [2, 4, 5, 14, 16, 17],
                    'Box2': [4, 5, 6, 7, 16, 17, 18, 19],
                    'Box3': [6, 7, 9, 10, 18, 19, 21, 22],
                    'Box4': [9, 10, 11, 12, 21, 22],
                    'Box5': [11, 12, 14],
                },
            },
            {
                rootNote: Note.G,
                allowedFretNumbersPerBox: {
                    'Box1': [3, 5, 6, 15, 17, 18],
                    'Box2': [5, 6, 7, 8, 17, 18, 19, 20],
                    'Box3': [7, 8, 10, 11, 19, 20, 22],
                    'Box4': [10, 11, 12, 13],
                    'Box5': [0, 1, 3, 12, 13, 15],
                },
            },
            {
                rootNote: Note.GSharp,
                allowedFretNumbersPerBox: {
                    'Box1': [4, 6, 7, 16, 18, 19],
                    'Box2': [6, 7, 8, 9, 18, 19, 20, 21],
                    'Box3': [8, 9, 11, 12, 20, 21],
                    'Box4': [11, 12, 13, 14],
                    'Box5': [1, 2, 4, 13, 14, 16],
                },
            },
        ];

        testCases.forEach(({ rootNote, allowedFretNumbersPerBox }) => {
            it(`should group ${rootNote} Pentatonic Minor into correct boxes`, () => {
                const scaleNotes = getScaleNotes(rootNote, Scale.PentatonicMinor);

                // Check each box
                Object.entries(allowedFretNumbersPerBox).forEach(([boxKey, allowedFretNumbers]) => {
                    const boxNotes = scaleNotes[boxKey];
                    expect(boxNotes, `Box ${boxKey} should exist`).toBeDefined();

                    // Get all fret numbers in this box
                    const actualFretNumbers = boxNotes.map(note => note.position.fretNumber);

                    // Check that all fret numbers are in the allowed set
                    const invalidFretNumbers = actualFretNumbers.filter(
                        fret => !allowedFretNumbers.includes(fret)
                    );

                    expect(
                        invalidFretNumbers,
                        `Box ${boxKey} for ${rootNote} Pentatonic Minor should only have allowed fret numbers. Found invalid frets: ${invalidFretNumbers.join(', ')}`
                    ).toHaveLength(0);

                    // Also check that we have the expected fret numbers (not missing any)
                    const uniqueActualFrets = [...new Set(actualFretNumbers)].sort((a, b) => a - b);
                    const expectedFrets = [...allowedFretNumbers].sort((a, b) => a - b);

                    expect(
                        uniqueActualFrets,
                        `Box ${boxKey} for ${rootNote} should have all expected fret numbers`
                    ).toEqual(expectedFrets);
                });
            });
        });
    });
});
