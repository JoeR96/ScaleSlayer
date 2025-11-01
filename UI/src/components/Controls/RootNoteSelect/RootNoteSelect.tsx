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

    const containerStyle: React.CSSProperties = {
        display: 'grid',
        gridTemplateColumns: 'repeat(4, 1fr)',
        gap: '6px',
        width: '100%',
        maxWidth: '100%',
    };

    const getButtonStyle = (note: Note): React.CSSProperties => ({
        padding: '7px 10px',
        fontSize: '13px',
        fontWeight: '600',
        border: note === selectedRootNote ? '1.5px solid #ff8c00' : '1.5px solid rgba(255, 255, 255, 0.15)',
        borderRadius: '6px',
        backgroundColor: note === selectedRootNote ? '#ff8c00' : 'rgba(255, 255, 255, 0.03)',
        color: note === selectedRootNote ? 'white' : 'rgba(255, 255, 255, 0.87)',
        cursor: 'pointer',
        transition: 'all 0.15s ease',
        minHeight: '34px',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
    });

    return (
        <div style={containerStyle}>
            {notes.map((note) => (
                <button
                    key={note}
                    onClick={() => handleRootNoteClick(note)}
                    style={getButtonStyle(note)}
                    onMouseEnter={(e) => {
                        if (note !== selectedRootNote) {
                            e.currentTarget.style.backgroundColor = 'rgba(255, 255, 255, 0.1)';
                            e.currentTarget.style.borderColor = 'rgba(255, 255, 255, 0.3)';
                        }
                    }}
                    onMouseLeave={(e) => {
                        if (note !== selectedRootNote) {
                            e.currentTarget.style.backgroundColor = 'rgba(255, 255, 255, 0.05)';
                            e.currentTarget.style.borderColor = 'rgba(255, 255, 255, 0.2)';
                        }
                    }}
                >
                    {note}
                </button>
            ))}
        </div>
    );
};

export default RootNoteSelect;
