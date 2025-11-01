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

    const containerStyle: React.CSSProperties = {
        display: 'flex',
        flexDirection: 'column',
        width: '100%',
        maxWidth: '100%',
        gap: '16px',
        overflow: 'hidden',
        padding: '0',
        boxSizing: 'border-box',
    };

    const titleStyle: React.CSSProperties = {
        fontSize: 'clamp(22px, 3vw, 28px)',
        fontWeight: '600',
        letterSpacing: '-0.01em',
        margin: 0,
        color: 'rgba(255, 255, 255, 0.95)',
        textAlign: 'center',
    };

    const fretboardSectionStyle: React.CSSProperties = {
        width: '100%',
        display: 'flex',
        flexDirection: 'column',
        gap: '12px',
        alignItems: 'center',
        overflow: 'hidden',
    };

    const controlsSectionStyle: React.CSSProperties = {
        width: '100%',
        display: 'flex',
        flexDirection: 'column',
        gap: '12px',
        alignItems: 'center',
    };

    return (
        <>
            <div style={containerStyle} className="guitar-fretboard-container">
                {/* Title */}
                <h1 style={titleStyle}>Scale Slayer</h1>

                {/* Fretboard section - always second */}
                <div
                    style={fretboardSectionStyle}
                    className="fretboard-section"
                >
                    <FretboardSVG />
                    {showScaleNotes && <ScaleBoxSelect />}
                </div>

                {/* Controls section */}
                <div
                    style={controlsSectionStyle}
                    className="controls-section"
                >
                    <ControlPanel />
                    <Metronome />
                </div>
            </div>

            <style>{`
                .guitar-fretboard-container {
                    max-width: 1600px;
                    margin: 0 auto;
                    padding: 12px;
                    box-sizing: border-box;
                }

                /* Desktop layout */
                @media (min-width: 1024px) {
                    .guitar-fretboard-container {
                        padding: 20px 24px;
                        gap: 20px !important;
                    }

                    .controls-section {
                        display: flex;
                        flex-direction: row;
                        gap: 16px;
                        align-items: flex-start;
                    }

                    .controls-section > .control-panel {
                        flex: 2;
                    }

                    .controls-section > div:last-child {
                        flex: 1;
                    }

                    .fretboard-section {
                        width: 100%;
                        overflow: hidden;
                    }
                }

                /* Tablet layout */
                @media (min-width: 768px) and (max-width: 1023px) {
                    .guitar-fretboard-container {
                        padding: 16px;
                        gap: 16px !important;
                    }

                    .fretboard-section {
                        width: 100%;
                        overflow: hidden;
                    }
                }

                /* Mobile layout */
                @media (max-width: 767px) {
                    .guitar-fretboard-container {
                        padding: 8px;
                        gap: 12px !important;
                    }

                    .fretboard-section {
                        overflow: hidden;
                    }
                }
            `}</style>
        </>
    );
};

export default GuitarFretboard;
