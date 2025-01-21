import React, { useState, useEffect } from 'react';
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

    const [scale, setScale] = useState(1);

    useEffect(() => {
        const handleResize = () => {
            // Adjust scale based on window size (keeping fretboard proportion)
            const scaleValue = Math.min(window.innerWidth / 1600, 1);  // Ensure scale doesn't exceed 1
            setScale(scaleValue);
        };

        window.addEventListener('resize', handleResize);
        handleResize(); // Run once on initial load

        return () => window.removeEventListener('resize', handleResize);
    }, []);

    if (!selectedNotes || !selectedScaleNotes) {
        return <p>Loading notes and chords...</p>;
    }

    return (
        <div id="app" style={{ width: "100%", height: "100%" }}>
            <div style={{ display: "flex", transform: `scale(${scale})`, transformOrigin: "top left", height: '100%' }}>
                <div style={{ flex: 2, padding: "20px", width: "100%" }}>
                    <FretboardSVG />
                </div>
                <div style={{ flex: 1, height: '100%' }}>
                    <div style={{
                        display: "flex",
                        flexDirection: "column",
                        justifyContent: "space-between",
                        alignItems: "center",
                        paddingLeft: 50,
                        height: '100%',
                    }}>
                        <ControlPanel />
             
                    </div>
                </div>
            </div>
        </div>
    );
};

export default GuitarFretboard;
