import React from 'react';
import { useControlsBoundedStore } from "../Controls/ControlsBoundedStore";
import { fetchNotesHook } from "../../hooks/fetchNotesHook";
import { fetchScaleNotesHook } from "../../hooks/fetchScaleNotesHook";
import ScaleBoxSelect from "../Controls/ScaleBoxSelect/ScaleBoxSelect";
import FretboardSVG from "./FretboardSVG";
import ControlPanel from "../Controls/ControlPanel";
import Metronome from "../Metronome/Metronome";

const GuitarFretboard: React.FC = () => {
    const { selectedNotes, selectedScaleNotes,  showScaleNotes } = useControlsBoundedStore();
    fetchNotesHook();
    fetchScaleNotesHook();
    
    if (!selectedNotes || !selectedScaleNotes) {
        return <p>Loading notes and chords...</p>;
    }
    
    //This is not the way, but this styling will do for now until I figure out a proper design
    return (
        <div id="app">
            <h2 style={{
                fontSize: 'clamp(1.75rem, 6vw, 4rem)',
                fontWeight: 'bold',
                color: 'white',
                textAlign: 'center',
                textTransform: 'uppercase',
                letterSpacing: '0.1em',
                textShadow: '2px 2px 4px rgba(0, 0, 0, 0.7)',
                margin: '20px 0',
                background: 'linear-gradient(90deg, #ff8c00, #ff4500)',
                WebkitBackgroundClip: 'text',
                WebkitTextFillColor: 'transparent',
            }}>
                Scale Slayer
            </h2>

            <div style={{ display: "flex", width: '100%' }}>
                <div
                    // Fix the invalid style and avoid oversized width
                    style={{
                        flex: 0.8,
                        padding: 20,
                        width: '100%',
                        maxWidth: 1440,
                        margin: '0 auto'
                    }}
                >
                    <FretboardSVG/>
                    {/* Wrap controls so they scale together */}
                    <div
                        style={{
                            display: 'flex',
                            flexDirection: 'column',
                            gap: 16,
                            // This makes all nested text/UI scale with viewport, but within sensible bounds
                            fontSize: 'clamp(0.9rem, 1.4vw, 1rem)',
                            // Keep control content from stretching too wide relative to the SVG
                            maxWidth: 'min(100%, 100%)',
                            marginTop: 12,
                        }}
                    >
                        {showScaleNotes && <ScaleBoxSelect/>}
                        <ControlPanel/>
                        <div style={{ padding: 10 }}>
                            <Metronome/>
                        </div>
                    </div>
                </div>
                <div style={{ flex: 0.2 }}>
                    <div style={{
                        display: "flex",
                        flexDirection: "column",
                        justifyContent: "space-between",
                        alignItems: "center",
                        paddingLeft: 50,
                    }}>
                        {/* right column content */}
                    </div>
                </div>
            </div>

        </div>
    );


};

export default GuitarFretboard;
