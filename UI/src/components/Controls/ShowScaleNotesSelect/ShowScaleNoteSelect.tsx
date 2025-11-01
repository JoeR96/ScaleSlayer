import React from 'react';
import { useControlsBoundedStore } from "../ControlsBoundedStore";

const ShowScaleNoteSelect = () => {
    const { showScaleNotes, setShowScaleNote } = useControlsBoundedStore();

    const options = [
        { label: "Show All Notes", shortLabel: "All", value: false },
        { label: "Show Scale Notes", shortLabel: "Scale", value: true },
    ];

    const handleToggle = (value: boolean) => {
        setShowScaleNote(value);
    };

    const containerStyle: React.CSSProperties = {
        display: 'grid',
        gridTemplateColumns: 'repeat(2, 1fr)',
        gap: '4px',
        width: '100%',
        maxWidth: '100%',
    };

    const getButtonStyle = (value: boolean): React.CSSProperties => ({
        padding: '7px 10px',
        fontSize: '12px',
        fontWeight: '600',
        border: value === showScaleNotes ? '1.5px solid #ff8c00' : '1.5px solid rgba(255, 255, 255, 0.15)',
        borderRadius: '6px',
        backgroundColor: value === showScaleNotes ? '#ff8c00' : 'rgba(255, 255, 255, 0.03)',
        color: value === showScaleNotes ? 'white' : 'rgba(255, 255, 255, 0.87)',
        cursor: 'pointer',
        transition: 'all 0.15s ease',
        minHeight: '32px',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        whiteSpace: 'nowrap',
    });

    return (
        <div style={containerStyle}>
            {options.map((option) => (
                <button
                    key={String(option.value)}
                    onClick={() => handleToggle(option.value)}
                    style={getButtonStyle(option.value)}
                    title={option.label}
                    onMouseEnter={(e) => {
                        if (option.value !== showScaleNotes) {
                            e.currentTarget.style.backgroundColor = 'rgba(255, 255, 255, 0.08)';
                            e.currentTarget.style.borderColor = 'rgba(255, 255, 255, 0.25)';
                        }
                    }}
                    onMouseLeave={(e) => {
                        if (option.value !== showScaleNotes) {
                            e.currentTarget.style.backgroundColor = 'rgba(255, 255, 255, 0.03)';
                            e.currentTarget.style.borderColor = 'rgba(255, 255, 255, 0.15)';
                        }
                    }}
                >
                    {option.shortLabel}
                </button>
            ))}
        </div>
    );
};

export default ShowScaleNoteSelect;
