import React, { useState, useEffect } from "react";
import IDoorData from "../types/Door";
import DoorService from "../services/DoorService";

const Door = ({ id, label, isOpen, isLocked, retrieveDoorsHandler }: IDoorData) => {

    const [door, setDoor] = useState<IDoorData>({
        id: id,
        label: label,
        isOpen: isOpen,
        isLocked: isLocked,
        retrieveDoorsHandler: retrieveDoorsHandler
    });

    const changeLabel = (e: React.ChangeEvent<HTMLInputElement>) => {

        const { value } = e.target;

        door.label = value;
        update(door);
    }

    const changeIsOpen = (e: React.ChangeEvent<HTMLInputElement>) => {
        door.isOpen = e.target.checked;
        update(door);
    }

    const changeIsLocked = (e: React.ChangeEvent<HTMLInputElement>) => {
        door.isLocked = e.target.checked;
        update(door);
    }

    const update = (door: IDoorData) => {
        DoorService.update(id, door).then(response => {
            setDoor(response.data);
            console.log(response.data);
        }).catch(e => {
            console.log(e);
        });
    }

    const removeDoor = (e: React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
        var el = e.target as HTMLInputElement;
        var id = el.getAttribute("data-id");

        DoorService.remove(id).then(response => {
            retrieveDoorsHandler();
            console.log(response.data);
        }).catch(e => {
            console.log(e);
        });
    }

    return (
        <tr>
            <td><input id={door.id} type="text" value={door.label} onChange={e => changeLabel(e)} /></td>
            <td><input type="checkbox" value="Open" checked={door.isOpen === true} onChange={e => changeIsOpen(e)} /></td>
            <td><input type="checkbox" value="Locked" checked={door.isLocked === true} onChange={e => changeIsLocked(e)} /></td>
            <td><button type="button" data-id={door.id} onClick={e => removeDoor(e)}>Remove</button></td>
        </tr>
    );
}
export default Door;