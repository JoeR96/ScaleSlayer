import React from 'react';  // This should resolve the issue if you're using an older build setup.
import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import './index.css';
import GuitarFretboard from "./components/Fretboard/GuitarFretboard";

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
