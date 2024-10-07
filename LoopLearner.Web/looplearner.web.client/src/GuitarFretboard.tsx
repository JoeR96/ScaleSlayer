import React, { useEffect, useState } from 'react';

// Define types for the notes, positions, chords, and chord notes
interface NotePosition {
    stringNumber: number;
    fretNumber: number;
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

// Function to generate colors or styles based on note name
const getNoteColor = (note: string) => {
    const colors: { [key: string]: string } = {
        'C': '#f94144',
        'CSharp': '#f3722c',
        'D': '#f8961e',
        'DSharp': '#f9844a',
        'E': '#f9c74f',
        'F': '#90be6d',
        'FSharp': '#43aa8b',
        'G': '#4d908e',
        'GSharp': '#577590',
        'A': '#277da1',
        'ASharp': '#2a9d8f',
        'B': '#264653'
    };
    return colors[note] || '#000000'; // Default to black if note name isn't recognized
};

// Function to format the note name for display (e.g., "FSharp" becomes "F#")
const formatnote = (note: string) => {
    if (note.includes('Sharp')) {
        return note[0] + '#'; // Example: FSharp becomes F#
    }
    return note[0]; // Otherwise just return the first letter
};

// Main GuitarFretboard component
const GuitarFretboard: React.FC = () => {
    const [notes, setNotes] = useState<GuitarNote[] | undefined>(undefined);
    const [scaleNotes, setScaleNotes] = useState<ScaleBox | undefined>(undefined);
    const [chords, setChords] = useState<Chord[] | undefined>(undefined);
    const [selectedBoxes, setSelectedBoxes] = useState<string[]>([]);
    const [showScale, setShowScale] = useState<boolean>(false);
    const [selectedRootNote, setSelectedRootNote] = useState<Note>(Note.CSharp);
    const [selectedScaleType, setSelectedScaleType] = useState<ScaleType>(ScaleType.PentatonicMinor);

    // Fetch the notes data
    useEffect(() => {
        const populateNotesData = async () => {
            try {
                const response = await fetch('api/songs/notes');
                if (response.ok) {
                    const data = await response.json();
                    setNotes(data);
                } else {
                    console.error('Failed to fetch notes:', response.statusText);
                }
            } catch (error) {
                console.error('Error fetching notes:', error);
            }
        };

        const populateScaleData = async () => {
            try {
                const response = await fetch(`api/scales/${selectedRootNote}-${selectedScaleType}`);

                if (response.ok) {
                    // Read the response body as JSON
                    const data = await response.json();
                    console.log('Fetched scale data:', data); // Log the actual data
                    setScaleNotes(data);
                } else {
                    console.error('Failed to fetch scale notes:', response.statusText);
                }
            } catch (error) {
                console.error('Error fetching scale notes:', error);
            }
        };



        const populateScaleAndChordsData = async () => {
            try {
                const scaleResponse = await fetch(`api/scales/${selectedRootNote}-${selectedScaleType}`);
                if (scaleResponse.ok) {
                    const scaleData = await scaleResponse.json();
                    setScaleNotes(scaleData);
                } else {
                    console.error('Failed to fetch scale notes:', scaleResponse.statusText);
                }

                const chordsResponse = await fetch(`api/scales/${selectedRootNote}-${selectedScaleType}-chords`);
                if (chordsResponse.ok) {
                    const chordsData = await chordsResponse.json();
                    setChords(chordsData);
                } else {
                    console.error('Failed to fetch chords:', chordsResponse.statusText);
                }
            } catch (error) {
                console.error('Error fetching scale and chords data:', error);
            }
        };

        const populateChordsData = async () => {
            try {
                const response = await fetch('api/scales/csharp-minor-pentatonic-chords');
                if (response.ok) {
                    const data = await response.json();
                    setChords(data);
                } else {
                    console.error('Failed to fetch chords:', response.statusText);
                }
            } catch (error) {
                console.error('Error fetching chords:', error);
            }
        };
        
        populateNotesData();
        populateScaleData();
        populateChordsData();
    }, [selectedRootNote, selectedScaleType]);

    const handleToggle = () => {
        setShowScale(!showScale);
    };

    const handleBoxChange = (box: string) => {
        if (selectedBoxes.includes(box)) {
            setSelectedBoxes(selectedBoxes.filter((b) => b !== box));
        } else {
            setSelectedBoxes([...selectedBoxes, box]);
        }
    };

    const handleSelectAllBoxes = (e: React.ChangeEvent<HTMLInputElement>) => {
        if (e.target.checked) {
            setSelectedBoxes(['Box1', 'Box2', 'Box3', 'Box4', 'Box5']);
        } else {
            setSelectedBoxes([]);
        }
    };

    const strings = ['E', 'A', 'D', 'G', 'B', 'E'].reverse();
    const totalFrets = 23;
    const fretboardWidth = 800;

    if (!notes || !scaleNotes || !chords) {
        return <p>Loading notes and chords...</p>;
    }

    const notesToDisplay = showScale
        ? selectedBoxes.flatMap((box) => scaleNotes[box as keyof ScaleBox])
        : notes;

    const getFretRange = (chord: Chord) => {
        const frets = chord.notes.map((note) => note.position.fretNumber);
        return {
            lowestFret: Math.min(...frets),
            highestFret: Math.max(...frets)
        };
    };

    const handleRootNoteChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        setSelectedRootNote(e.target.value);
    };

    const handleScaleTypeChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        setSelectedScaleType(e.target.value);
    };
    
    const renderChordFretboard = (chord: Chord) => {
        const { lowestFret, highestFret } = getFretRange(chord);
        const fretRange = [...Array(highestFret - lowestFret + 1).keys()].map(f => f + lowestFret);
        const chordFretboardWidth = 200;
        const chordFretWidth = chordFretboardWidth / fretRange.length;

        return (
            <svg key={chord.rootNote + chord.chordType} width={chordFretboardWidth} height="120" xmlns="http://www.w3.org/2000/svg">
                {/* Draw string labels in white */}
                {strings.map((stringLabel, index) => (
                    <text
                        key={index}
                        x="10"
                        y={15 + index * 20 + 4}
                        style={{ fill: 'white', fontWeight: 'bold' }}  // Use inline style
                        fontSize="10"
                        textAnchor="middle"
                    >
                        {stringLabel}
                    </text>
                ))}

                {/* Draw strings */}
                {strings.map((string, index) => (
                    <line
                        key={index}
                        x1="30"
                        y1={15 + index * 20}
                        x2={chordFretboardWidth}
                        y2={15 + index * 20}
                        stroke="black"
                        strokeWidth="2"
                    />
                ))}

                {/* Draw frets and fret numbers */}
                {fretRange.map((fret, index) => (
                    <g key={index}>
                        <line
                            x1={index * chordFretWidth}
                            y1="0"
                            x2={index * chordFretWidth}
                            y2="120"
                            stroke="black"
                            strokeWidth="1"
                        />
                        <text
                            x={index * chordFretWidth + chordFretWidth / 2}
                            y="12"
                            style={{ fill: 'white', fontWeight: 'bold' }}  // Use inline style
                            fontSize="10"
                            textAnchor="middle"
                        >
                            {fret}
                        </text>
                    </g>
                ))}

                {/* Draw chord notes */}
                {chord.notes.map((note, index) => (
                    <g key={index}>
                        <circle
                            cx={(note.position.fretNumber - lowestFret) * chordFretWidth + chordFretWidth / 2}
                            cy={15 + (note.position.stringNumber - 1) * 20}
                            r="8"
                            fill={getNoteColor(note.note)}
                            stroke="black"
                            strokeWidth="2"
                        />
                        <text
                            x={(note.position.fretNumber - lowestFret) * chordFretWidth + chordFretWidth / 2}
                            y={15 + (note.position.stringNumber - 1) * 20 + 4}
                            fill="white"
                            fontSize="8"
                            textAnchor="middle"
                        >
                            {formatnote(note.note)}
                        </text>
                    </g>
                ))}
            </svg>
        );
    };

    return (
        <div id="app">
            <h2>Guitar Fretboard</h2>
            <div>
                <label>Root Note: </label>
                <select value={selectedRootNote} onChange={handleRootNoteChange}>
                    <option value="C">C</option>
                    <option value="CSharp">C#</option>
                    <option value="D">D</option>
                    <option value="DSharp">D#</option>
                    <option value="E">E</option>
                    <option value="F">F</option>
                    <option value="FSharp">F#</option>
                    <option value="G">G</option>
                    <option value="GSharp">G#</option>
                    <option value="A">A</option>
                    <option value="ASharp">A#</option>
                    <option value="B">B</option>
                </select>

                <label> Scale Type: </label>
                <select value={selectedScaleType} onChange={handleScaleTypeChange}>
                    <option value="PentatonicMinor">Pentatonic Minor</option>
                    <option value="Major">Major</option>
                    <option value="Minor">Minor</option>
                    {/* Add more scale types as needed */}
                </select>
            </div>
            <button onClick={handleToggle}>
                {showScale ? 'Show All Notes' : 'Show C# Minor Pentatonic Scale'}
            </button>

            {showScale && (
                <div>
                    <label>
                        <input
                            type="checkbox"
                            onChange={handleSelectAllBoxes}
                            checked={selectedBoxes.length === 5}
                        />
                        Select All Boxes
                    </label>

                    <div>
                        {['Box1', 'Box2', 'Box3', 'Box4', 'Box5'].map((box) => (
                            <label key={box}>
                                <input
                                    type="checkbox"
                                    value={box}
                                    checked={selectedBoxes.includes(box)}
                                    onChange={() => handleBoxChange(box)}
                                />
                                {box}
                            </label>
                        ))}
                    </div>
                </div>
            )}

            <svg width={fretboardWidth} height="200" viewBox={`0 0 ${fretboardWidth} 200`}
                 xmlns="http://www.w3.org/2000/svg">
                {/* Draw string labels in white */}
                {strings.map((stringLabel, index) => (
                    <text
                        key={index}
                        x="10"
                        y={30 + index * 25 + 4}
                        style={{fill: 'white', fontWeight: 'bold'}}  // Use inline style
                        fontSize="12"
                        textAnchor="middle"
                    >
                        {stringLabel}
                    </text>
                ))}

                {/* Draw strings */}
                {strings.map((_, index) => (
                    <line
                        key={index}
                        x1="30"
                        y1={30 + index * 25}
                        x2={fretboardWidth}
                        y2={30 + index * 25}
                        stroke="black"
                        strokeWidth="2"
                    />
                ))}

                {/* Draw frets and fret numbers */}
                {[...Array(totalFrets).keys()].map((fret, index) => (
                    <g key={index}>
                        <line
                            x1={index * (fretboardWidth / totalFrets)}
                            y1="0"
                            x2={index * (fretboardWidth / totalFrets)}
                            y2="200"
                            stroke="black"
                            strokeWidth="1"
                        />
                        <text
                            x={index * (fretboardWidth / totalFrets) + (fretboardWidth / totalFrets) / 2}
                            y="12"
                            style={{fill: 'white', fontWeight: 'bold'}}  // Use inline style
                            fontSize="10"
                            textAnchor="middle"
                        >
                            {fret}
                        </text>
                    </g>
                ))}

                {notesToDisplay.map((note, index) => (
                    <g key={index}>
                        <circle
                            cx={note.position.fretNumber * (fretboardWidth / totalFrets) + (fretboardWidth / totalFrets) / 2}
                            cy={30 + (note.position.stringNumber - 1) * 25}
                            r="10"
                            fill={getNoteColor(note.note)}
                            stroke="black"
                            strokeWidth="2"
                        >
                            <title>{note.note}</title>
                        </circle>

                        <text
                            x={note.position.fretNumber * (fretboardWidth / totalFrets) + (fretboardWidth / totalFrets) / 2}
                            y={30 + (note.position.stringNumber - 1) * 25 + 4}
                            fill="white"
                            fontSize="10"
                            textAnchor="middle"
                            style={{userSelect: 'none'}}
                        >
                            {formatnote(note.note)}
                        </text>
                    </g>
                ))}
            </svg>

            {showScale && (
                <>
                    <h3>C# Minor Pentatonic Chords</h3>
                    <div style={{display: 'flex', flexWrap: 'wrap', gap: '10px'}}>
                        {chords.map((chord) => (
                            <div key={chord.rootNote + chord.chordType} style={{textAlign: 'center', width: '220px'}}>
                                <h4>
                                    {chord.rootNote} {chord.chordType}
                                    {chord.chordExtension !== 'None' ? ` ${chord.chordExtension}` : ''}
                                </h4>
                                {renderChordFretboard(chord)}
                            </div>
                        ))}
                    </div>
                </>
            )}
        </div>
    );
};

export default GuitarFretboard;

// TypeScript equivalent of C# Note enum
export enum Note {
    A = "A",
    ASharp = "ASharp",
    B = "B",
    C = "C",
    CSharp = "CSharp",
    D = "D",
    DSharp = "DSharp",
    E = "E",
    F = "F",
    FSharp = "FSharp",
    G = "G",
    GSharp = "GSharp"
}

// TypeScript equivalent of C# ScaleType enum
export enum ScaleType {
    Major = "Major",
    Minor = "Minor",
    HarmonicMinor = "HarmonicMinor",
    MelodicMinor = "MelodicMinor",
    PentatonicMajor = "PentatonicMajor",
    PentatonicMinor = "PentatonicMinor",
    Blues = "Blues",
    Dorian = "Dorian",
    Phrygian = "Phrygian",
    Lydian = "Lydian",
    Mixolydian = "Mixolydian",
    Locrian = "Locrian"
}
