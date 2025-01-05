import { StateCreator } from "zustand";

export interface ScaleNotesSelectSlice {
    selectedScaleNotes: ScaleBox;
    setSelectedScaleNotes: (scaleNotes: ScaleBox) => void;
}

export const createBoundedScaleNoteSelectSlice: StateCreator<
    ScaleNotesSelectSlice,
    [],
    [],
    ScaleNotesSelectSlice
> = (set) => ({
    selectedScaleNotes: { },
    setSelectedScaleNotes: (scaleNotes) => set({ selectedScaleNotes: scaleNotes }),
});
