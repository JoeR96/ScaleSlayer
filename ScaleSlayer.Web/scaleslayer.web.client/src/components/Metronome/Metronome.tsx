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

    return (
        <div style={{paddingLeft: '50px'}}>
            <h2>Metronome</h2>
            <div style={{ display: 'flex', alignItems: 'center',  marginTop: '10px' }}>
                <label>
                    BPM:
                    <input
                        type="number"
                        value={bpm}
                        min={30}
                        max={300}
                        onChange={(e) => setBpm(Number(e.target.value))}
                        style={{ marginLeft: '5px', marginRight: '5px' }}
                    />
                </label>
                <button onClick={toggleMetronome} style={{ padding: '5px 10px' }}>
                    {isPlaying ? 'Stop' : 'Start'}
                </button>
            </div>
        </div>
    );
};

export default Metronome;
