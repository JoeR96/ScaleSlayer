import React from 'react';
import {formatNote, getNoteColor} from "../../utils/noteUtils";

const NotesDisplay: React.FC<{ notes: GuitarNotes }> = ({ notes }) => {
    return (
        <>
            {notes.notes.map((note, index) => (
                <g key={index}>
                    <circle
                        cx={note.position.fretNumber * (1600 / 23) + (1600 / 23) / 2}
                        cy={45 + (note.position.stringNumber - 1) * 50}
                        r="20"
                        fill={getNoteColor(note.note)}
                        stroke="black"
                        strokeWidth="4"
                    >
                        <title>{note.note}</title>
                    </circle>

                    <text
                        x={note.position.fretNumber * (1600 / 23) + (1600 / 23) / 2}
                        y={45 + (note.position.stringNumber - 1) * 50 + 6}
                        fill="white"
                        fontSize="16"
                        textAnchor="middle"
                        style={{ userSelect: 'none' }}
                    >
                        {formatNote(note.note)}
                    </text>
                </g>
            ))}
        </>
    );
};

export default NotesDisplay;
