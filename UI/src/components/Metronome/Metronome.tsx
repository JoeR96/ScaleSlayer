import React, { useState } from 'react';
import * as Tone from 'tone';

const Metronome: React.FC = () => {
    const [bpm, setBpm] = useState(120);
    const [isPlaying, setIsPlaying] = useState(false);

    const toggleMetronome = async () => {
        if (!isPlaying) {
            await Tone.start();
            const synth = new Tone.MembraneSynth().toDestination();
            const loop = new Tone.Loop((time) => {
                synth.triggerAttackRelease('C2', '8n', time);
            }, '4n').start(0);
            Tone.Transport.bpm.value = bpm;
            Tone.Transport.start();
        } else {
            Tone.Transport.stop();
            Tone.Transport.cancel();
        }
        setIsPlaying(!isPlaying);
    };

    const containerStyle: React.CSSProperties = {
        display: 'flex',
        flexDirection: 'row',
        gap: '8px',
        padding: '14px',
        background: 'rgba(255, 255, 255, 0.02)',
        borderRadius: '10px',
        border: '1px solid rgba(255, 255, 255, 0.08)',
        alignItems: 'center',
        justifyContent: 'center',
        backdropFilter: 'blur(10px)',
    };

    const inputStyle: React.CSSProperties = {
        padding: '7px 10px',
        fontSize: '13px',
        borderRadius: '6px',
        border: '1.5px solid rgba(255, 255, 255, 0.15)',
        backgroundColor: 'rgba(255, 255, 255, 0.03)',
        color: 'rgba(255, 255, 255, 0.87)',
        width: '65px',
        textAlign: 'center',
        fontWeight: '600',
    };

    const buttonStyle: React.CSSProperties = {
        padding: '7px 20px',
        fontSize: '13px',
        fontWeight: '600',
        borderRadius: '6px',
        border: '1.5px solid #ff8c00',
        backgroundColor: isPlaying ? '#ff8c00' : 'rgba(255, 140, 0, 0.05)',
        color: isPlaying ? 'white' : '#ff8c00',
        cursor: 'pointer',
        transition: 'all 0.15s ease',
        minHeight: '34px',
        whiteSpace: 'nowrap',
    };

    const labelStyle: React.CSSProperties = {
        fontSize: '12px',
        color: 'rgba(255, 255, 255, 0.5)',
        fontWeight: '600',
    };

    return (
        <div style={containerStyle}>
            <span style={labelStyle}>BPM</span>
            <input
                type="number"
                value={bpm}
                min={30}
                max={300}
                onChange={(e) => setBpm(Number(e.target.value))}
                style={inputStyle}
            />
            <button
                onClick={toggleMetronome}
                style={buttonStyle}
                onMouseEnter={(e) => {
                    if (!isPlaying) {
                        e.currentTarget.style.backgroundColor = 'rgba(255, 140, 0, 0.1)';
                    }
                }}
                onMouseLeave={(e) => {
                    if (!isPlaying) {
                        e.currentTarget.style.backgroundColor = 'rgba(255, 140, 0, 0.05)';
                    }
                }}
            >
                {isPlaying ? 'Stop' : 'Start'}
            </button>
        </div>
    );
};

export default Metronome;
