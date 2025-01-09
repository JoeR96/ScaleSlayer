import { StateCreator } from "zustand";
import { Scale } from "../../../enums/enums";
import {create} from "zustand/index";

export interface ScaleSelectSlice {
    selectedScale: Scale;
    setSelectedScale: (scale: Scale) => void;
}

export const createBoundedScaleSelectSlice: StateCreator<
    ScaleSelectSlice,
    [],
    [],
    ScaleSelectSlice
> = (set) => ({
    selectedScale: Scale.PentatonicMinor,
    setSelectedScale: (scale) => set({ selectedScale: scale }),
});
