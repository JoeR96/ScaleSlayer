import React from 'react';
import {formatNote, getNoteColor} from "../../utils/noteUtils";
import {useControlsBoundedStore} from "../Controls/ControlsBoundedStore";


const FretboardSVG: React.FC = ({ }) => {

    const { showScaleNotes, selectedScaleNotes, selectedScaleBoxes, selectedNotes  } = useControlsBoundedStore();
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

    const containerStyle: React.CSSProperties = {
        width: '100%',
        maxWidth: '100%',
        overflow: 'hidden',
        display: 'flex',
        justifyContent: 'center',
        padding: '4px',
        WebkitOverflowScrolling: 'touch',
        boxSizing: 'border-box',
    };

    const svgWrapperStyle: React.CSSProperties = {
        width: '100%',
        maxWidth: '1400px',
        display: 'flex',
        justifyContent: 'center',
    };

    return (
        <div style={containerStyle}>
            <div style={svgWrapperStyle}>
                <svg
                    width="100%"
                    height="100%"
                    viewBox={`-40 0 ${fretboardWidth + 40} ${fretboardHeight}`}
                    preserveAspectRatio="xMidYMid meet"
                    xmlns="http://www.w3.org/2000/svg"
                    style={{
                        maxWidth: '100%',
                        width: '100%',
                        height: 'auto',
                    }}
                    className="fretboard-svg"
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
                            stroke="white"
                            strokeWidth="3"
                            opacity="0.6"
                        />
                    ))}

                    {[...Array(totalFrets).keys()].map((fret, index) => (
                        <g key={index}>
                            <line
                                x1={index * (fretboardWidth / totalFrets)}
                                y1="0"
                                x2={index * (fretboardWidth / totalFrets)}
                                y2={fretboardHeight - 50}
                                stroke="white"
                                strokeWidth="2"
                                opacity="0.4"
                            />
                            <text
                                x={index * (fretboardWidth / totalFrets) + (fretboardWidth / totalFrets) / 2}
                                y="24"
                                style={{ fill: 'white', fontWeight: 'bold' }}
                                fontSize="18"
                                textAnchor="middle"
                                opacity="0.7"
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
                                    stroke="rgba(0, 0, 0, 0.3)"
                                    strokeWidth="3"
                                    style={{
                                        filter: 'drop-shadow(0 2px 4px rgba(0, 0, 0, 0.3))',
                                        cursor: 'pointer'
                                    }}
                                >
                                    <title>{note.note}</title>
                                </circle>

                                <text
                                    x={note.position.fretNumber * (fretboardWidth / totalFrets) + (fretboardWidth / totalFrets) / 2}
                                    y={45 + (note.position.stringNumber - 1) * 50 + 6 + 25}
                                    fill="white"
                                    fontSize="14"
                                    fontWeight="bold"
                                    textAnchor="middle"
                                    style={{
                                        userSelect: 'none',
                                        pointerEvents: 'none',
                                        textShadow: '0 1px 2px rgba(0, 0, 0, 0.5)'
                                    }}
                                >
                                    {formatNote(note.note)}
                                </text>
                            </g>
                        );
                    })}
                </svg>
            </div>

            <style>{`
                .fretboard-svg {
                    min-width: 100%;
                }

                @media (min-width: 768px) {
                    .fretboard-svg {
                        min-width: 700px;
                    }
                }

                @media (min-width: 1024px) {
                    .fretboard-svg {
                        min-width: 1000px;
                    }
                }

                @media (min-width: 1400px) {
                    .fretboard-svg {
                        min-width: 1200px;
                    }
                }
            `}</style>
        </div>
    );
};

export default FretboardSVG;
