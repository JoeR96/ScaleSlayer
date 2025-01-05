import { useEffect } from 'react';
import {useControlsBoundedStore} from "../components/Controls/ControlsBoundedStore";

export const fetchScaleNotesHook = () => {
    const { selectedScaleNotes, setSelectedScaleNotes, selectedRootNote, selectedScale } = useControlsBoundedStore();

    useEffect(() => {
        const fetchScaleNotes = async () => {
            try {
                const scaleResponse = await fetch(`api/scales/${selectedRootNote}/${selectedScale}`);
                if (scaleResponse.ok) {
                    const scaleData = await scaleResponse.json();
                    setSelectedScaleNotes(scaleData.scaleNotes);
                } else {
                }
            } catch (error) {
            }
        };

        if (selectedRootNote && selectedScale) {
            fetchScaleNotes().catch((error) => console.error('Error fetching scale notes:', error));
        }
    }, [selectedRootNote, selectedScale]); 

    return selectedScaleNotes;
};
