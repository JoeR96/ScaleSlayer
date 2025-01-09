interface NotePosition {
    stringNumber: number;
    fretNumber: number;
}

interface GuitarNotes {
    notes: GuitarNote[];
}
interface GuitarNote {
    note: string;
    position: NotePosition;
}

interface ScaleBox {
    [box: string]: GuitarNote[];
}

interface Chord {
    rootNote: string;
    chordType: string;
    chordExtension: string;
    notes: GuitarNote[];
}