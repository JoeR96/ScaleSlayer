import React, { useState } from 'react';
import * as Tone from 'tone';

const Metronome: React.FC = () => {
    const [bpm, setBpm] = useState(120);
    const [isPlaying, setIsPlaying] = useState(false);

    const toggleMetronome = async () => {
        if (!isPlaying) {
            await Tone.start(); // Required for browser interaction
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

    return (
        <div>
            <h2>Metronome</h2>
            <label>
                BPM:
                <input
                    type="number"
                    value={bpm}
                    min={30}
                    max={300}
                    onChange={(e) => setBpm(Number(e.target.value))}
                />
            </label>
            <button onClick={toggleMetronome}>
                {isPlaying ? 'Stop' : 'Start'}
            </button>
        </div>
    );
};

export default Metronome;
