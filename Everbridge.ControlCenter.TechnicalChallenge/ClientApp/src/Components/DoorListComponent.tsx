import React from 'react';
import { Door, DoorProps } from './DoorComponent';

export const DoorList = (doors: DoorProps[]) {
    for (let door of doors) {
        <Door id={door.id} label={door.label} isOpen={door.isOpen} isLocked={door.isLocked} />
    }
}