import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import GuitarFretboard from "./components/Fretboard/GuitarFretboard";
import Metronome from "./components/Metronome/Metronome";

const rootElement = document.getElementById('root');
if (rootElement) {
    createRoot(rootElement).render(
        <StrictMode>
            <GuitarFretboard />
        </StrictMode>
    );
} else {
    console.error('Root element not found, something naughty happened :(');
}
