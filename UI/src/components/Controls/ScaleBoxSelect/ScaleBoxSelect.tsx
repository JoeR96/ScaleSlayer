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

    return (
        <div style={{  display: 'grid', gridTemplateColumns: 'repeat(5, 1fr)', gap: '10px' }}>
            {['Box1', 'Box2', 'Box3', 'Box4', 'Box5'].map((box) => (
                <button
                    key={box}
                    onClick={() => handleBoxChange(box)}
                    style={{
                        fontSize: '16px',
                        fontWeight: 'bold',
                        borderRadius: '5px',
                        backgroundColor: selectedScaleBoxes.includes(box) ? 'orange' : '#f0f0f0',
                        color: selectedScaleBoxes.includes(box) ? 'white' : 'black',
                        border: selectedScaleBoxes.includes(box) ? '2px solid orange' : '2px solid gray',
                        cursor: 'pointer',
                        textAlign: 'center',
                    }}
                >
                    {box}
                </button>
            ))}
        </div>
    );
};

export default ScaleBoxSelect;
