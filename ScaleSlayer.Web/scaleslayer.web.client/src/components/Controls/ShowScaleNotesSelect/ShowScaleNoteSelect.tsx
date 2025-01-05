import React from 'react';
import {useControlsBoundedStore} from "../ControlsBoundedStore";

const ShowScaleNoteSelect = () => {

    const { showScaleNotes, setShowScaleNote  } = useControlsBoundedStore();

    const handleToggle = () => {
        setShowScaleNote(!showScaleNotes);
    };
    
    return (
        <div>
            <label> Show Scale Notes: </label>
            <select onChange={handleToggle} style={{marginBottom: '1em'}}>
                <option value="false" selected={!showScaleNotes}>Show All Notes</option>
                <option value="true" selected={showScaleNotes}>Show Scale Notes</option>
            </select>
        </div>
    );
};

export default ShowScaleNoteSelect;

