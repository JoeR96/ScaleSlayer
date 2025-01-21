import React from 'react';
import { formatNote, getNoteColor } from "../../utils/noteUtils";
import { useControlsBoundedStore } from "../Controls/ControlsBoundedStore";
import ControlPanel from "../Controls/ControlPanel";
import ScaleBoxSelect from "../Controls/ScaleBoxSelect/ScaleBoxSelect";

const FretboardSVG: React.FC = () => {
    const { showScaleNotes, selectedScaleNotes, selectedScaleBoxes, selectedNotes } = useControlsBoundedStore();
    const strings = ['E', 'A', 'D', 'G', 'B', 'E'].reverse();
    const totalFrets = 23;
    const fretboardWidth = 1000;
    const fretboardHeight = 400;

    const notesToDisplay: GuitarNotes = showScaleNotes
        ? (() => {
            const notes = Object.keys(selectedScaleNotes).flatMap((box) => {
                if (selectedScaleBoxes.includes(box)) {
                    const scaleNotes = selectedScaleNotes[box as keyof ScaleBox];
                    return Array.isArray(scaleNotes) ? scaleNotes : [];
                }
                return [];
            });

            return { notes };
        })()
        : { notes: selectedNotes.notes || [] };

    return (<>
        <svg
            width="auto"  // Set width and height to 100% for responsiveness
            height="auto"
            viewBox={`-40 0 ${fretboardWidth + 40} ${fretboardHeight}`}
            xmlns="http://www.w3.org/2000/svg"
        >
            {strings.map((stringLabel, index) => (
                <text
                    key={index}
                    x="-22.5"
                    y={75 + index * 50 + 4}
                    style={{ fill: 'white', fontWeight: 'bold' }}
                    fontSize="24"
                    textAnchor="middle"
                >
                    {stringLabel}
                </text>
            ))}

            {strings.map((_, index) => (
                <line
                    key={index}
                    x1="-60"
                    y1={45 + index * 50}
                    x2={fretboardWidth}
                    y2={45 + index * 50}
                    stroke="black"
                    strokeWidth="4"
                />
            ))}

            {[...Array(totalFrets).keys()].map((fret, index) => (
                <g key={index}>
                    <line
                        x1={index * (fretboardWidth / totalFrets)}
                        y1="0"
                        x2={index * (fretboardWidth / totalFrets)}
                        y2={fretboardHeight - 50}
                        stroke="black"
                        strokeWidth="2"
                    />
                    <text
                        x={index * (fretboardWidth / totalFrets) + (fretboardWidth / totalFrets) / 2}
                        y="24"
                        style={{ fill: 'white', fontWeight: 'bold' }}
                        fontSize="20"
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
                            cy={45 + (note.position.stringNumber - 1) * 50 + 25}
                            r="15"
                            fill={getNoteColor(note.note)}
                            stroke="black"
                            strokeWidth="4"
                        >
                            <title>{note.note}</title>
                        </circle>

                        <text
                            x={note.position.fretNumber * (fretboardWidth / totalFrets) + (fretboardWidth / totalFrets) / 2}
                            y={45 + (note.position.stringNumber - 1) * 50 + 6 + 25}
                            fill="white"
                            fontSize="16"
                            textAnchor="middle"
                            style={{ userSelect: 'none' }}
                        >
                            {formatNote(note.note)}
                        </text>
                    </g>
                );
            })}
        </svg>
            {showScaleNotes && <ScaleBoxSelect />}
        </>
);
};

export default FretboardSVG;
