import { StateCreator } from "zustand";
import { Scale } from "../../../enums/enums";
import {create} from "zustand/index";
import {useState} from "react";

export interface ShowScaleNotesSelectSlice {
    showScaleNotes: boolean;
    setShowScaleNote: (setShowScaleNote: boolean) => void;
}


export const createBoundedShowScaleNotesSelectSlice: StateCreator<
    ShowScaleNotesSelectSlice,
    [],
    [],
    ShowScaleNotesSelectSlice
> = (set) => ({
    showScaleNotes: true,
    setShowScaleNote: (setShowScaleNote) => set({ showScaleNotes: setShowScaleNote }),
});


