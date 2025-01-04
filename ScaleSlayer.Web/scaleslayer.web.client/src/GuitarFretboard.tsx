import React, { useEffect, useState } from 'react';
import {Note, ScaleType} from "./enums";

const GuitarFretboard: React.FC = () => {
    const [notes, setNotes] = useState<GuitarNotes | undefined>(undefined);
    const [scaleNotes, setScaleNotes] = useState<ScaleBox | undefined>(undefined);
    const [selectedBoxes, setSelectedBoxes] = useState<string[]>([]);
    const [showScale, setShowScale] = useState<boolean>(false);
    const [selectedRootNote, setSelectedRootNote] = useState<Note>(Note.CSharp);
    const [selectedScaleType, setSelectedScaleType] = useState<ScaleType>(ScaleType.PentatonicMinor);

    useEffect(() => {
        (async () => {
            try {
                const notesResponse = await fetch('api/notes/notes');
                if (notesResponse.ok) {
                    const notesData = await notesResponse.json();
                    setNotes(notesData);
                } else {
                    console.error('Failed to fetch notes:', notesResponse.statusText);
                }

                const scaleResponse = await fetch(`api/scales/${selectedRootNote}/${selectedScaleType}`);
                if (scaleResponse.ok) {
                    const scaleData = await scaleResponse.json();
                    console.log('Fetched scale data:', scaleData); // Log the actual data
                    console.log(scaleData.scaleNotes);
                    setScaleNotes(scaleData.scaleNotes);
                } else {
                    console.error('Failed to fetch scale notes:', scaleResponse.statusText);
                }
            } catch (error) {
                console.error('Error in useEffect:', error);
            }
        })();
    }, [selectedRootNote, selectedScaleType]);


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

    const formatNote = (note: string) => {
        if (note.includes('Sharp')) {
            return note[0] + '#';
        }
        return note[0];
    };

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

    const strings = ['E', 'A', 'D', 'G', 'B', 'E'].reverse();
    const totalFrets = 23;
    const fretboardWidth = 1600; // Twice the original width
    const fretboardHeight = 400; // Twice the original height

    if (!notes || !scaleNotes) {
        return <p>Loading notes and chords...</p>;
    }

    const notesToDisplay: GuitarNotes = showScale
        ? { notes: selectedBoxes.flatMap((box) => scaleNotes[box as keyof ScaleBox]) }
        : notes || { notes: [] };

    const handleRootNoteChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        setSelectedRootNote(e.target.value as Note);
    };

    const handleScaleTypeChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        setSelectedScaleType(e.target.value as ScaleType);
    };

    return (
        <div id="app">
            <h2>Guitar Fretboard</h2>
            <div style={{ marginBottom: '1em' }}>
                <label>Root Note: </label>
                <select value={selectedRootNote} onChange={handleRootNoteChange} style={{ margin: '0 1em 1em 0' }}>
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
                <select value={selectedScaleType} onChange={handleScaleTypeChange} style={{ margin: '0 1em 1em 0' }}>
                    <option value="PentatonicMinor">Pentatonic Minor</option>
                    <option value="Major">Major</option>
                    <option value="Minor">Minor</option>
                </select>

                <select onChange={handleToggle} style={{ marginBottom: '1em' }}>
                    <option value="false" selected={!showScale}>Show All Notes</option>
                    <option value="true" selected={showScale}>Show Scale Notes</option>
                </select>
            </div>

            <svg width={fretboardWidth} height={fretboardHeight} viewBox={`-40 0 ${fretboardWidth + 40} ${fretboardHeight}`}
                 xmlns="http://www.w3.org/2000/svg">
                {strings.map((stringLabel, index) => (
                    <text
                        key={index}
                        x="-30"
                        y={45 + index * 50 + 4} // Adjusted for height change
                        style={{fill: 'white', fontWeight: 'bold'}}
                        fontSize="24" // Larger text for better visibility
                        textAnchor="middle"
                    >
                        {stringLabel}
                    </text>
                ))}

                {strings.map((_, index) => (
                    <line
                        key={index}
                        x1="60"
                        y1={45 + index * 50}
                        x2={fretboardWidth}
                        y2={45 + index * 50}
                        stroke="black"
                        strokeWidth="4" // Thicker lines for better visibility
                    />
                ))}

                {[...Array(totalFrets).keys()].map((fret, index) => (
                    <g key={index}>
                        <line
                            x1={index * (fretboardWidth / totalFrets)}
                            y1="0"
                            x2={index * (fretboardWidth / totalFrets)}
                            y2={fretboardHeight}
                            stroke="black"
                            strokeWidth="2"
                        />
                        <text
                            x={index * (fretboardWidth / totalFrets) + (fretboardWidth / totalFrets) / 2}
                            y="24" // Adjusted for height change
                            style={{fill: 'white', fontWeight: 'bold'}}
                            fontSize="20" // Larger text for better visibility
                            textAnchor="middle"
                        >
                            {fret}
                        </text>
                    </g>
                ))}
                {notesToDisplay.notes.map((note, index) => {
                    return (
                        <g key={index}>
                            <circle
                                cx={note.position.fretNumber * (fretboardWidth / totalFrets) + (fretboardWidth / totalFrets) / 2}
                                cy={45 + (note.position.stringNumber - 1) * 50}
                                r="20" // Larger circles for better visibility
                                fill={getNoteColor(note.note)}
                                stroke="black"
                                strokeWidth="4" // Thicker stroke for better visibility
                            >
                                <title>{note.note}</title>
                            </circle>

                            <text
                                x={note.position.fretNumber * (fretboardWidth / totalFrets) + (fretboardWidth / totalFrets) / 2}
                                y={45 + (note.position.stringNumber - 1) * 50 + 6} // Adjusted for height change
                                fill="white"
                                fontSize="16" // Larger text for better visibility
                                textAnchor="middle"
                                style={{userSelect: 'none'}}
                            >
                                {formatNote(note.note)}
                            </text>
                        </g>
                    );
                })}

            </svg>

            {showScale && (
                <div>
                    <div style={{ marginTop: '1em' }}>
                        {['Box1', 'Box2', 'Box3', 'Box4', 'Box5'].map((box) => (
                            <label key={box} style={{ marginRight: '10px' }}>
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
        </div>
    );
};

export default GuitarFretboard;
