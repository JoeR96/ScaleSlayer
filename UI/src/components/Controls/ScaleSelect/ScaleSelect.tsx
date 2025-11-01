import React from 'react';
import {Scale} from "../../../enums/enums";
import {useControlsBoundedStore} from "../ControlsBoundedStore";

const ScaleSelect = () => {
    const { selectedScale, setSelectedScale } = useControlsBoundedStore();

    const scales: { label: string; shortLabel: string; value: Scale }[] = [
        { label: "Pentatonic Minor", shortLabel: "Pent Min", value: Scale.PentatonicMinor },
        { label: "Major", shortLabel: "Major", value: Scale.Major },
        { label: "Minor", shortLabel: "Minor", value: Scale.Minor },
    ];

    const handleScaleClick = (scale: Scale) => {
        setSelectedScale(scale);
    };

    const containerStyle: React.CSSProperties = {
        display: 'flex',
        flexDirection: 'column',
        gap: '4px',
        width: '100%',
        maxWidth: '100%',
    };

    const getButtonStyle = (scale: Scale): React.CSSProperties => ({
        padding: '7px 10px',
        fontSize: '12px',
        fontWeight: '600',
        border: scale === selectedScale ? '1.5px solid #ff8c00' : '1.5px solid rgba(255, 255, 255, 0.15)',
        borderRadius: '6px',
        backgroundColor: scale === selectedScale ? '#ff8c00' : 'rgba(255, 255, 255, 0.03)',
        color: scale === selectedScale ? 'white' : 'rgba(255, 255, 255, 0.87)',
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
            {scales.map((scale) => (
                <button
                    key={scale.value}
                    onClick={() => handleScaleClick(scale.value)}
                    style={getButtonStyle(scale.value)}
                    title={scale.label}
                    onMouseEnter={(e) => {
                        if (scale.value !== selectedScale) {
                            e.currentTarget.style.backgroundColor = 'rgba(255, 255, 255, 0.08)';
                            e.currentTarget.style.borderColor = 'rgba(255, 255, 255, 0.25)';
                        }
                    }}
                    onMouseLeave={(e) => {
                        if (scale.value !== selectedScale) {
                            e.currentTarget.style.backgroundColor = 'rgba(255, 255, 255, 0.03)';
                            e.currentTarget.style.borderColor = 'rgba(255, 255, 255, 0.15)';
                        }
                    }}
                >
                    {scale.shortLabel}
                </button>
            ))}
        </div>
    );
};

export default ScaleSelect;
