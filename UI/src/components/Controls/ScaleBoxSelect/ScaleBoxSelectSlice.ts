import { StateCreator } from "zustand";
import {create} from "zustand/index";

export interface ScaleBoxSelectSlice {
    selectedScaleBoxes: string[];
    setSelectedScaleBoxes: (scaleBoxes: string[]) => void;
}

export const createScaleBoxSelectSlice = create<ScaleBoxSelectSlice>((set) => ({
    selectedScaleBoxes: [],
    setSelectedScaleBoxes: (scaleBoxes) => set({ selectedScaleBoxes: scaleBoxes }),
}));

export const createBoundedScaleBoxSelectSlice: StateCreator<
    ScaleBoxSelectSlice,
    [],
    [],
    ScaleBoxSelectSlice
> = (set) => ({
    selectedScaleBoxes: ["Box2", "Box4"],
    setSelectedScaleBoxes: (scaleBoxes) => set({ selectedScaleBoxes: scaleBoxes }),
});
