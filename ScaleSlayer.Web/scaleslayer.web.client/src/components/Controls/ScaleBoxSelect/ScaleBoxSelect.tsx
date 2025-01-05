
import React from 'react';
import {useControlsBoundedStore} from "../ControlsBoundedStore";

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
        <div>
            <div style={{marginTop: '1em'}}>
                {['Box1', 'Box2', 'Box3', 'Box4', 'Box5'].map((box) => (
                    <label key={box} style={{marginRight: '10px'}}>
                        <input
                            type="checkbox"
                            value={box}
                            checked={selectedScaleBoxes.includes(box)}
                            onChange={() => handleBoxChange(box)}
                        />
                        {box}
                    </label>
                ))}
            </div>
        </div>
    );
};

export default ScaleBoxSelect;

