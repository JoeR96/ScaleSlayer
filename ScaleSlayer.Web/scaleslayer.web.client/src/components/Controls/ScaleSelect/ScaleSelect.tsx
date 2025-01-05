import React from 'react';
import { Scale } from "../../../enums/enums";
import {useControlsBoundedStore} from "../ControlsBoundedStore";

const ScaleSelect = () => {

    const handleScaleChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        setSelectedScale(e.target.value as Scale);
    };

    const { selectedScale, setSelectedScale } = useControlsBoundedStore();
    
    return (
        <div>
            <label> Scale Type: </label>
            <select value={selectedScale} onChange={handleScaleChange} style={{margin: '0 1em 1em 0'}}>
                <option value="PentatonicMinor">Pentatonic Minor</option>
                <option value="Major">Major</option>
                <option value="Minor">Minor</option>
            </select>
        </div>
    );
};

export default ScaleSelect;

