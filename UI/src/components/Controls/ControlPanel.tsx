import React from 'react';
import ScaleSelect from "./ScaleSelect/ScaleSelect";
import RootNoteSelect from "./RootNoteSelect/RootNoteSelect";
import ShowScaleNoteSelect from "./ShowScaleNotesSelect/ShowScaleNoteSelect";

const ControlPanel: React.FC = ({  }) => {
    return (
        <div style={{ marginBottom: '1em' }}>
            <RootNoteSelect />
            <ScaleSelect />
            <ShowScaleNoteSelect/>
        </div>
    );
};

export default ControlPanel;
