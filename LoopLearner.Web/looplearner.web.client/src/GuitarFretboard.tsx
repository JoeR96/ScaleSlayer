import React, { useEffect, useState } from 'react';
import {Note, ScaleType} from "./enums";


const GuitarFretboard: React.FC = () => {
    const [notes, setNotes] = useState<GuitarNote[] | undefined>(undefined);
    const [scaleNotes, setScaleNotes] = useState<ScaleBox | undefined>(undefined);
    const [selectedBoxes, setSelectedBoxes] = useState<string[]>([]);
    const [showScale, setShowScale] = useState<boolean>(false);
    const [selectedRootNote, setSelectedRootNote] = useState<Note>(Note.CSharp);
    const [selectedScaleType, setSelectedScaleType] = useState<ScaleType>(ScaleType.PentatonicMinor);

    useEffect(() => {
        const populateNotesData = async () => {
            try {
                const response = await fetch('api/notes/notes');
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
        
        populateNotesData();
        populateScaleData();
        
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

    const formatnote = (note: string) => {
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

    if (!notes || !scaleNotes) {
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

            <svg width={fretboardWidth} height="200" viewBox={`0 0 ${fretboardWidth} 200`}
                 xmlns="http://www.w3.org/2000/svg">
                {strings.map((stringLabel, index) => (
                    <text
                        key={index}
                        x="0"
                        y={30 + index * 25 + 4}
                        style={{fill: 'white', fontWeight: 'bold'}}
                        fontSize="12"
                        textAnchor="middle"
                    >
                        {stringLabel}
                    </text>
                ))}

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


