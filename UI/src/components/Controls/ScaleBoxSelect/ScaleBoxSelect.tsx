import React from 'react';
import { useControlsBoundedStore } from "../ControlsBoundedStore";

const ScaleBoxSelect = () => {
    const { selectedScaleBoxes, setSelectedScaleBoxes } = useControlsBoundedStore();

    const handleBoxChange = (box: string) => {
        if (selectedScaleBoxes.includes(box)) {
            setSelectedScaleBoxes(selectedScaleBoxes.filter((b) => b !== box));
        } else {
            setSelectedScaleBoxes([...selectedScaleBoxes, box]);
        }
    };

    const containerStyle: React.CSSProperties = {
        display: 'grid',
        gridTemplateColumns: 'repeat(5, 1fr)',
        gap: '10px',
        width: '100%',
        padding: '16px',
        background: 'rgba(255, 255, 255, 0.03)',
        borderRadius: '12px',
        border: '1px solid rgba(255, 255, 255, 0.1)',
    };

    const getButtonStyle = (box: string): React.CSSProperties => ({
        padding: '12px',
        fontSize: '15px',
        fontWeight: 'bold',
        borderRadius: '8px',
        backgroundColor: selectedScaleBoxes.includes(box) ? '#ff8c00' : 'rgba(255, 255, 255, 0.05)',
        color: selectedScaleBoxes.includes(box) ? 'white' : 'rgba(255, 255, 255, 0.87)',
        border: selectedScaleBoxes.includes(box) ? '2px solid #ff8c00' : '2px solid rgba(255, 255, 255, 0.2)',
        cursor: 'pointer',
        textAlign: 'center',
        transition: 'all 0.2s ease',
        minHeight: '48px',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
    });

    return (
        <div style={containerStyle}>
            {['Box1', 'Box2', 'Box3', 'Box4', 'Box5'].map((box) => (
                <button
                    key={box}
                    onClick={() => handleBoxChange(box)}
                    style={getButtonStyle(box)}
                    onMouseEnter={(e) => {
                        if (!selectedScaleBoxes.includes(box)) {
                            e.currentTarget.style.backgroundColor = 'rgba(255, 255, 255, 0.1)';
                            e.currentTarget.style.borderColor = 'rgba(255, 255, 255, 0.3)';
                        }
                    }}
                    onMouseLeave={(e) => {
                        if (!selectedScaleBoxes.includes(box)) {
                            e.currentTarget.style.backgroundColor = 'rgba(255, 255, 255, 0.05)';
                            e.currentTarget.style.borderColor = 'rgba(255, 255, 255, 0.2)';
                        }
                    }}
                >
                    {box.replace('Box', '')}
                </button>
            ))}
            <style>{`
                @media (max-width: 600px) {
                    div[style*="gridTemplateColumns"] {
                        grid-template-columns: repeat(5, minmax(50px, 1fr)) !important;
                    }
                }
            `}</style>
        </div>
    );
};

export default ScaleBoxSelect;
