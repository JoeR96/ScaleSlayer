export const getNoteColor  = (note: string) => {
    const colors: { [key: string]: string } = {
        'C': '#f94144',
        'CSharp': '#f3722c',
        'D': '#f8961e',
        'DSharp': '#f9844a',
        'E': '#f9c74f',
        'F': '#90be6d',
        'FSharp': '#43aa8b',
        'G': '#4d908e',
        'GSharp': '#577590',
        'A': '#277da1',
        'ASharp': '#2a9d8f',
        'B': '#264653'
    };
    return colors[note] || '#000000'; 
};

export const formatNote = (note: string) => {
    if (note.includes('Sharp')) {
        return note[0] + '#';
    }
    return note[0];
};