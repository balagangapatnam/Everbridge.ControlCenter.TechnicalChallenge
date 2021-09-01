import React, { useState, useEffect } from "react";
import IDoorData from "../types/Door";
import DoorService from "../services/DoorService";

const Door = ({ id, label, isOpen, isLocked }: IDoorData) => {

    const changeLabel = (e: React.ChangeEvent<HTMLInputElement>) => {

        let door: IDoorData = {
            id: id,
            label: e.target.textContent ?? label,
            isOpen: isOpen,
            isLocked: isLocked
        };

        DoorService.update(id, door);
    }

    const changeIsOpen = (e: React.ChangeEvent<HTMLInputElement>) => {
        let door: IDoorData = {
            id: id,
            label: label,
            isOpen: e.target.checked,
            isLocked: isLocked
        };
        DoorService.update(id, door);
    }

    const changeIsLocked = (e: React.ChangeEvent<HTMLInputElement>) => {
        let door: IDoorData = {
            id: id,
            label: label,
            isOpen: isOpen,
            isLocked: e.target.checked
        };
        DoorService.update(id, door);
    }

    return (
        <tr>
            <td><input id={id} type="text" value={label} onChange={e => changeLabel(e)} /></td>
            <td><input type="checkbox" value="Open" checked={isOpen === true} onChange={e => changeIsOpen(e)} /></td>
            <td><input type="checkbox" value="Locked" checked={isLocked === true} onChange={e => changeIsLocked(e)} /></td>
        </tr>
    );
}
export default Door;