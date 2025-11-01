// Scale calculation engine - ported from C# Scale.cs

import { ALL_FRETBOARD_NOTES, FretboardNote } from './fretboardNotes';
import { Note, Scale } from '../enums/enums';

interface FretRange {
    minFret: number;
    maxFret: number;
}

// Scale intervals (semitones from root note)
const SCALE_INTERVALS: Record<Scale, number[]> = {
    [Scale.PentatonicMinor]: [0, 3, 5, 7, 10],
    [Scale.Major]: [0, 2, 4, 5, 7, 9, 11],
    [Scale.Minor]: [0, 2, 3, 5, 7, 8, 10],
};

// Chromatic scale starting from A
const CHROMATIC_SCALE: Note[] = [
    Note.A, Note.ASharp, Note.B, Note.C, Note.CSharp, Note.D,
    Note.DSharp, Note.E, Note.F, Note.FSharp, Note.G, Note.GSharp
];

// Chromatic scale starting from E (for offset calculation)
const CHROMATIC_SCALE_FOR_OFFSET: Note[] = [
    Note.E, Note.F, Note.FSharp, Note.G, Note.GSharp, Note.A,
    Note.ASharp, Note.B, Note.C, Note.CSharp, Note.D, Note.DSharp, Note.E
];

// Box positions and their fret offsets
const SCALE_BOX_INTERVALS: Record<string, { startOffset: number; endOffset: number }> = {
    'Box1': { startOffset: 0, endOffset: 3 },
    'Box2': { startOffset: 2, endOffset: 5 },
    'Box3': { startOffset: 4, endOffset: 8 },
    'Box4': { startOffset: 7, endOffset: 10 },
    'Box5': { startOffset: 9, endOffset: 12 },
};

/**
 * Get all note names in a scale
 */
export function getScaleNoteNames(rootNote: Note, scaleType: Scale): Note[] {
    const intervals = SCALE_INTERVALS[scaleType];
    if (!intervals) {
        throw new Error(`Invalid scale type: ${scaleType}`);
    }

    const rootIndex = CHROMATIC_SCALE.indexOf(rootNote);
    if (rootIndex === -1) {
        throw new Error(`Invalid root note: ${rootNote}`);
    }

    return intervals.map(interval => {
        const noteIndex = (rootIndex + interval) % CHROMATIC_SCALE.length;
        return CHROMATIC_SCALE[noteIndex];
    });
}

/**
 * Get all fretboard notes that match the given note names
 */
export function getFretNotesByNames(noteNames: Note[]): FretboardNote[] {
    const noteNamesSet = new Set(noteNames);
    return ALL_FRETBOARD_NOTES.filter(fretNote =>
        noteNamesSet.has(fretNote.note as Note)
    );
}

/**
 * Get root note offset for box calculations
 */
function getRootNoteOffset(rootNote: Note): number {
    const index = CHROMATIC_SCALE_FOR_OFFSET.indexOf(rootNote);
    if (index === -1) {
        throw new Error(`Invalid root note for offset: ${rootNote}`);
    }
    return index;
}

/**
 * Resolve fret ranges for a specific box
 */
function resolveFretRangesForBox(
    boxInterval: { startOffset: number; endOffset: number },
    rootNoteOffset: number,
    totalFrets: number = 22 // Highest fret number (frets go from 0-22)
): FretRange[] {
    let startFret = rootNoteOffset + boxInterval.startOffset;
    let endFret = rootNoteOffset + boxInterval.endOffset;

    const fretRanges: FretRange[] = [];

    // Add main range - match C# logic exactly
    if (startFret < totalFrets && endFret <= totalFrets) {
        fretRanges.push({ minFret: startFret, maxFret: endFret });
    } else {
        fretRanges.push({ minFret: startFret, maxFret: totalFrets });
    }

    // Handle fret wrap-around (going down the neck)
    // Modify startFret and endFret in place (matching C# behavior)
    while (startFret > 0) {
        startFret -= 12;
        endFret -= 12;

        if (startFret >= 0 && endFret > 0) {
            fretRanges.push({ minFret: startFret, maxFret: endFret });
        }
    }

    // Handle fret wrap-around (going up the neck)
    // Use the modified startFret and endFret values
    let nextStartFret = startFret + 12;
    let nextEndFret = endFret + 12;
    while (nextStartFret < totalFrets) {
        fretRanges.push({ minFret: nextStartFret, maxFret: nextEndFret });
        nextStartFret += 12;
        nextEndFret += 12;
    }

    return fretRanges;
}

/**
 * Resolve box fret positions for all boxes
 */
function resolveBoxFretPositions(rootNote: Note): Record<string, FretRange[]> {
    const rootNoteOffset = getRootNoteOffset(rootNote);
    const boxes: Record<string, FretRange[]> = {};

    for (const [boxKey, boxInterval] of Object.entries(SCALE_BOX_INTERVALS)) {
        boxes[boxKey] = resolveFretRangesForBox(boxInterval, rootNoteOffset);
    }

    return boxes;
}

/**
 * Group notes into scale boxes
 */
export function groupNotesIntoBoxes(
    notes: FretboardNote[],
    rootNote: Note
): Record<string, FretboardNote[]> {
    const boxFretPositions = resolveBoxFretPositions(rootNote);
    const boxNotes: Record<string, FretboardNote[]> = {};

    for (const [boxKey, fretRanges] of Object.entries(boxFretPositions)) {
        boxNotes[boxKey] = notes.filter(note =>
            fretRanges.some(range =>
                note.position.fretNumber >= range.minFret &&
                note.position.fretNumber <= range.maxFret
            )
        );
    }

    return boxNotes;
}

/**
 * Get scale notes grouped by boxes (main entry point)
 */
export function getScaleNotes(rootNote: Note, scaleType: Scale): Record<string, FretboardNote[]> {
    const noteNames = getScaleNoteNames(rootNote, scaleType);
    const notes = getFretNotesByNames(noteNames);
    return groupNotesIntoBoxes(notes, rootNote);
}

/**
 * Get all notes (for "Show All Notes" mode)
 */
export function getAllNotes(): FretboardNote[] {
    return ALL_FRETBOARD_NOTES;
}
