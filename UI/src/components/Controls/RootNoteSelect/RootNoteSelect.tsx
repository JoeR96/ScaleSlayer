import React from 'react';
import {Note} from "../../../enums/enums";
import {useControlsBoundedStore} from "../ControlsBoundedStore";

const RootNoteSelect = () => {
    const { selectedRootNote, setSelectedRootNote } = useControlsBoundedStore();

    const notes: Note[] = [
        Note.C, Note.CSharp, Note.D, Note.DSharp, Note.E, Note.F,
        Note.FSharp, Note.G, Note.GSharp, Note.A, Note.ASharp, Note.B
    ];

    const handleRootNoteClick = (note: Note) => {
        setSelectedRootNote(note);
    };

    return (
        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(3, 1fr)', gap: '1em' }}>
            {notes.map((note) => (
                <button
                    key={note}
                    onClick={() => handleRootNoteClick(note)}
                    style={{
                        padding: '1px',
                        fontSize: '16px',
                        fontWeight: 'bold',
                        border: note === selectedRootNote ? '2px solid orange' : '2px solid gray',
                        borderRadius: '5px',
                        backgroundColor: note === selectedRootNote ? 'orange' : '#f0f0f0',
                        color: note === selectedRootNote ? 'white' : 'black',
                        cursor: 'pointer',
                    }}
                >
                    {note}
                </button>
            ))}
        </div>
    );
};

export default RootNoteSelect;
