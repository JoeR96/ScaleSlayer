import { create } from "zustand";
import {createBoundedScaleSelectSlice } from "./ScaleSelect/ScaleSelectSlice";
import {createBoundedRootNoteSelectSlice } from "./RootNoteSelect/RootNoteSelectSlice";
import {createBoundedNoteSelectSlice} from "./NotesSelectSlice";
import {createBoundedScaleNoteSelectSlice} from "./ScaleNotesSlice";
import {createBoundedScaleBoxSelectSlice} from "./ScaleBoxSelect/ScaleBoxSelectSlice";
import { createBoundedShowScaleNotesSelectSlice, } from "./ShowScaleNotesSelect/ShowScaleNoteSelectSlice";

type ControlsBoundedStore = ReturnType<typeof createBoundedScaleSelectSlice> &
    ReturnType<typeof createBoundedRootNoteSelectSlice> &
    ReturnType<typeof createBoundedNoteSelectSlice> &
    ReturnType<typeof createBoundedScaleNoteSelectSlice> &
    ReturnType<typeof createBoundedScaleBoxSelectSlice> &
    ReturnType<typeof createBoundedShowScaleNotesSelectSlice>;

export const useControlsBoundedStore = create<ControlsBoundedStore>((set, get, api) => ({
    ...createBoundedScaleSelectSlice(set, get, api),
    ...createBoundedRootNoteSelectSlice(set, get, api),
    ...createBoundedNoteSelectSlice(set, get, api),
    ...createBoundedScaleNoteSelectSlice(set, get, api),
    ...createBoundedScaleBoxSelectSlice(set, get, api),
    ...createBoundedShowScaleNotesSelectSlice(set, get, api),
}));
