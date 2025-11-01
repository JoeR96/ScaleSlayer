import { useEffect } from 'react';
import {useControlsBoundedStore} from "../components/Controls/ControlsBoundedStore";
import { getScaleNotes } from "../data/scaleEngine";

export const fetchScaleNotesHook = () => {
    const { selectedScaleNotes, setSelectedScaleNotes, selectedRootNote, selectedScale } = useControlsBoundedStore();

    useEffect(() => {
        if (selectedRootNote && selectedScale) {
            // Calculate scale notes using local engine instead of API
            const scaleNotes = getScaleNotes(selectedRootNote, selectedScale);
            setSelectedScaleNotes(scaleNotes);
        }
    }, [selectedRootNote, selectedScale, setSelectedScaleNotes]);

    return selectedScaleNotes;
};
