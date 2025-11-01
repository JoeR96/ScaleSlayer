import React from 'react';
import {Scale} from "../../../enums/enums";
import {useControlsBoundedStore} from "../ControlsBoundedStore";

const ScaleSelect = () => {
    const { selectedScale, setSelectedScale } = useControlsBoundedStore();

    const scales: { label: string; value: Scale }[] = [
        { label: "Pentatonic Minor", value: Scale.PentatonicMinor },
        { label: "Major - TBC", value: Scale.Major },
        { label: "Minor - TBC", value: Scale.Minor },
    ];

    const handleScaleClick = (scale: Scale) => {
        setSelectedScale(scale);
    };

    return (
        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(3, 1fr)', gap: '1em', margin: '1em 0' }}>
            {scales.map((scale) => (
                <button
                    key={scale.value}
                    onClick={() => handleScaleClick(scale.value)}
                    style={{
                        padding: '1px',
                        fontSize: '16px',
                        fontWeight: 'bold',
                        border: scale.value === selectedScale ? '2px solid gray' : '2px solid gray',
                        borderRadius: '5px',
                        backgroundColor: scale.value === selectedScale ? 'orange' : '#f0f0f0',
                        color: scale.value === selectedScale ? 'white' : 'black',
                        cursor: 'pointer',
                    }}
                >
                    {scale.label}
                </button>
            ))}
        </div>
    );
};

export default ScaleSelect;
