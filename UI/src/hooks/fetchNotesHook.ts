import { useEffect } from 'react';
import {useControlsBoundedStore} from "../components/Controls/ControlsBoundedStore";
import { getAllNotes } from "../data/scaleEngine";

export const fetchNotesHook = () => {
    const { selectedNotes, setSelectedNotes } = useControlsBoundedStore();

    useEffect(() => {
        // Load notes from local data instead of API
        const allNotes = getAllNotes();
        setSelectedNotes({ notes: allNotes });
    }, [setSelectedNotes]);

    return selectedNotes;
};
