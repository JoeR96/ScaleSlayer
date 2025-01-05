import React from 'react';
import { useControlsBoundedStore } from "../Controls/ControlsBoundedStore";
import { fetchNotesHook } from "../../hooks/fetchNotesHook";
import { fetchScaleNotesHook } from "../../hooks/fetchScaleNotesHook";
import ScaleBoxSelect from "../Controls/ScaleBoxSelect/ScaleBoxSelect";
import FretboardSVG from "./FretboardSVG";
import ControlPanel from "../Controls/ControlPanel";
import Metronome from "../Metronome/Metronome";

const GuitarFretboard: React.FC = () => {
    const { selectedNotes, selectedScaleNotes, selectedScaleBoxes, showScaleNotes } = useControlsBoundedStore();
    fetchNotesHook();
    fetchScaleNotesHook();
    
    if (!selectedNotes || !selectedScaleNotes) {
        return <p>Loading notes and chords...</p>;
    }
    
    //This is not the way, but this styling will do for now until I figure out a proper design
    return (
        <div id="app">
            <h2>Guitar Fretboard</h2>
            <div style={{display: "flex", flexDirection: "row", justifyContent: "space-between", alignItems: "center"}}>
                <div style={{ paddingBottom: 50, paddingRight: 50}}>
                    <Metronome/>
                </div>
                <ControlPanel/>
            </div>
            <FretboardSVG/>

            {showScaleNotes && <ScaleBoxSelect/>}
        </div>
    );
};

export default GuitarFretboard;
