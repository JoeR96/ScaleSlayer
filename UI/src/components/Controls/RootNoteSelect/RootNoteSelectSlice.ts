import {Note} from "../../../enums/enums";
import {StateCreator} from "zustand/index";

export interface RootNoteSelectSlice {
    selectedRootNote: Note;
    setSelectedRootNote: (note: Note) => void;
}

export const createBoundedRootNoteSelectSlice: StateCreator<
    RootNoteSelectSlice,
    [],
    [],
    RootNoteSelectSlice
> = (set) => ({
    selectedRootNote: Note.CSharp,
    setSelectedRootNote: (note) => set({ selectedRootNote: note }),
});
