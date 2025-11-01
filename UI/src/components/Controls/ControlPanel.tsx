import React from 'react';
import ScaleSelect from "./ScaleSelect/ScaleSelect";
import RootNoteSelect from "./RootNoteSelect/RootNoteSelect";
import ShowScaleNoteSelect from "./ShowScaleNotesSelect/ShowScaleNoteSelect";

const ControlPanel: React.FC = ({  }) => {
    const containerStyle: React.CSSProperties = {
        display: 'flex',
        flexDirection: 'column',
        gap: '12px',
        width: '100%',
        padding: '14px',
        background: 'rgba(255, 255, 255, 0.02)',
        borderRadius: '10px',
        border: '1px solid rgba(255, 255, 255, 0.08)',
        backdropFilter: 'blur(10px)',
    };

    const labelStyle: React.CSSProperties = {
        margin: 0,
        fontSize: '12px',
        color: 'rgba(255, 255, 255, 0.5)',
        fontWeight: '600',
        textTransform: 'uppercase',
        letterSpacing: '0.5px',
    };

    const rowStyle: React.CSSProperties = {
        display: 'flex',
        alignItems: 'center',
        gap: '8px',
        width: '100%',
        justifyContent: 'space-between',
    };

    return (
        <div style={containerStyle} className="control-panel">
            <div style={{ width: '100%', display: 'flex', flexDirection: 'column', gap: '8px' }}>
                <h3 style={labelStyle}>Root Note</h3>
                <RootNoteSelect />
            </div>

            <div style={rowStyle}>
                <div style={{ display: 'flex', flexDirection: 'column', gap: '6px', flex: 1 }}>
                    <h3 style={labelStyle}>Scale</h3>
                    <ScaleSelect />
                </div>
                <div style={{ display: 'flex', flexDirection: 'column', gap: '6px', flex: 1 }}>
                    <h3 style={labelStyle}>Mode</h3>
                    <ShowScaleNoteSelect />
                </div>
            </div>

            <style>{`
                /* Desktop layout */
                @media (min-width: 1024px) {
                    .control-panel {
                        padding: 16px !important;
                    }
                }
            `}</style>
        </div>
    );
};

export default ControlPanel;
