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
            <h2 style={{
                fontSize: '8rem',          
                fontWeight: 'bold',        
                color: 'white',            
                textAlign: 'center',       
                textTransform: 'uppercase',
                letterSpacing: '0.1em',    
                textShadow: '2px 2px 4px rgba(0, 0, 0, 0.7)', 
                margin: '20px 0',          
                paddingBottom: '100px',
                background: 'linear-gradient(90deg, #ff8c00, #ff4500)', 
                WebkitBackgroundClip: 'text', 
                WebkitTextFillColor: 'transparent', 
            }}>
                Scale Slayer
            </h2>


            <div style={{display: "flex"}}>
                <div style={{flex: 0.8, padding: "20px, width: 100%"}}>
                    <FretboardSVG/>
                    {showScaleNotes && <ScaleBoxSelect/>}

                </div>
                <div style={{flex: 0.2}}>
                    <div style={{
                        display: "flex",
                        flexDirection: "column",
                        justifyContent: "space-between",
                        alignItems: "center",
                        paddingLeft: 50,
                    }}>
                        <ControlPanel/>
                        <div style={{paddingBottom: 10, paddingRight: 50}}>
                            <Metronome/>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    );

};

export default GuitarFretboard;
