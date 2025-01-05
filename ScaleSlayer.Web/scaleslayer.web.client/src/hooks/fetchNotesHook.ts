import { useEffect } from 'react';
import {useControlsBoundedStore} from "../components/Controls/ControlsBoundedStore";

export const fetchNotesHook = () => {
    const { selectedNotes, setSelectedNotes, selectedRootNote, selectedScale } = useControlsBoundedStore();
    useEffect(() => {
        const fetchNotes = async () => {
            try {
                const notesResponse = await fetch('api/notes/notes');

                if (notesResponse.ok) {
                    const notesData = await notesResponse.json();
                    setSelectedNotes(notesData);
                } else {
                    console.error('Failed to fetch notes:', notesResponse.statusText);
                }
            } catch (error) {
                console.error('Error fetching notes:', error);
            }
        };

        fetchNotes();
    }, []);

    return selectedNotes;
};
