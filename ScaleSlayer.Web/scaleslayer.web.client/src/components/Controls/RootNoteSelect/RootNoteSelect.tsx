import React from 'react';
import {Note} from "../../../enums/enums";
import {useControlsBoundedStore} from "../ControlsBoundedStore";

const RootNoteSelect = () => {

    const handleRootNoteChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
        setSelectedRootNote(e.target.value as Note);
    };

    const { selectedRootNote, setSelectedRootNote } = useControlsBoundedStore();


    return (
        <div>
            <label>Root Note: </label>
            <select value={selectedRootNote} onChange={handleRootNoteChange} style={{margin: '0 1em 1em 0'}}>
                <option value="C">C</option>
                <option value="CSharp">C#</option>
                <option value="D">D</option>
                <option value="DSharp">D#</option>
                <option value="E">E</option>
                <option value="F">F</option>
                <option value="FSharp">F#</option>
                <option value="G">G</option>
                <option value="GSharp">G#</option>
                <option value="A">A</option>
                <option value="ASharp">A#</option>
                <option value="B">B</option>
            </select>
        </div>
    );
};

export default RootNoteSelect;
