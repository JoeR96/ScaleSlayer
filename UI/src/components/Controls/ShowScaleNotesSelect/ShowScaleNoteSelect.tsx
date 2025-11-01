import React from 'react';
import { useControlsBoundedStore } from "../ControlsBoundedStore";

const ShowScaleNoteSelect = () => {
    const { showScaleNotes, setShowScaleNote } = useControlsBoundedStore();

    const options = [
        { label: "Show All Notes", value: false },
        { label: "Show Scale Notes", value: true },
    ];

    const handleToggle = (value: boolean) => {
        setShowScaleNote(value);
    };

    return (
        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(2, 1fr)', gap: '1em', margin: '1em 0' }}>
            {options.map((option) => (
                <button
                    key={String(option.value)}
                    onClick={() => handleToggle(option.value)}
                    style={{
                        padding: '1px',
                        fontSize: '16px',
                        fontWeight: 'bold',
                        border: option.value === showScaleNotes ? '2px solid gray' : '2px solid gray',
                        borderRadius: '1px',
                        backgroundColor: option.value === showScaleNotes ? 'orange' : '#f0f0f0',
                        color: option.value === showScaleNotes ? 'white' : 'black',
                        cursor: 'pointer',
                    }}
                >
                    {option.label}
                </button>
            ))}
        </div>
    );
};

export default ShowScaleNoteSelect;
