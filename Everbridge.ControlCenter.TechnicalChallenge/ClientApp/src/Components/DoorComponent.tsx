import React from 'react';

export type DoorProps = {
    id: string,
    label: string,
    isOpen: boolean,
    isLocked: boolean
}

export const Door = ({ id, label, isOpen, isLocked }: DoorProps) =>
    <div>
        <input id={id} type="text" value={label} />
        <input type="checkbox" value="Open" checked={isOpen === true} />
        <input type="checkbox" value="Locked" checked={isLocked === true} />
    </div>