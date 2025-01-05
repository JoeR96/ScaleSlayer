import { StateCreator } from "zustand";

export interface NotesSelectSlice {
    selectedNotes: GuitarNotes;
    setSelectedNotes: (notes: GuitarNotes) => void;
}

export const createBoundedNoteSelectSlice: StateCreator<
    NotesSelectSlice,
    [],
    [],
    NotesSelectSlice
> = (set) => ({
    selectedNotes: { notes : []},
    setSelectedNotes: (notes) => set({ selectedNotes: notes }),
});
