import React, { useState, useEffect } from "react";
import DoorService from "../services/DoorService";
import Door from './DoorComponent';
import IDoorData from "../types/Door";

export const DoorList = () => {

    //let doors: Array<IDoorData> = [
    //    {
    //        id: "87c7ccf1-11db-43ce-94b9-8c9b1b714059",
    //        label: "Door 1",
    //        isLocked: false,
    //        isOpen: true
    //    },
    //    {
    //        id: "cc639847-8f28-4f28-84ec-0e2c9efee9b1",
    //        label: "Door 2",
    //        isLocked: true,
    //        isOpen: true
    //    },
    //    {
    //        id: "e72341d2-3d60-42da-87b3-2b1a4caf4dc2",
    //        label: "Door 3",
    //        isLocked: false,
    //        isOpen: true
    //    },
    //    {
    //        id: "fc12bbcf-f89c-4d11-81d0-f7a52da1bf6e",
    //        label: "Door 4",
    //        isLocked: true,
    //        isOpen: false
    //    },
    //    {
    //        id: "9e5f3574-6aff-479c-b782-f45c3d18a3ff",
    //        label: "Door 5",
    //        isLocked: true,
    //        isOpen: false
    //    },
    //    {
    //        id: "bebe6115-71d3-44b4-b314-fdfc7c83d15d",
    //        label: "Door 6",
    //        isLocked: false,
    //        isOpen: false
    //    }
    //];

    const [doors, setDoors] = useState<Array<IDoorData>>([]);

    useEffect(() => {
        retrieveDoors();
    }, []);

    const retrieveDoors = () => {
        DoorService.getAll()
            .then(response => {
                setDoors(response.data);
                console.log(response.data);
            })
            .catch(e => {
                console.log(e);
            });
    };

    const addDoor = () => {
        DoorService.create({
            id: "",
            label: "Temp. Label",
            isOpen: false,
            isLocked: false,
            retrieveDoorsHandler: retrieveDoors
        }).then(response => {
            retrieveDoors();
            console.log(response.data);
        }).catch(e => {
                console.log(e);
        });
    }

    return (
        <div>
            <h4> Doors </h4>
            <table className="table">
                <thead>
                    <tr>
                        <th scope="col">Label</th>
                        <th scope="col">Open</th>
                        <th scope="col">Locked</th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                    {
                        doors.length === 0
                            ? <tr><th>There are no doors in the database.</th></tr>
                            : doors.map((door) => {
                                return <Door key={door.id} id={door.id} label={door.label} isOpen={door.isOpen} isLocked={door.isLocked} retrieveDoorsHandler={retrieveDoors} />;
                            })
                    }
                </tbody>
            </table>
            <button type="button" onClick={addDoor}>Add Door</button>
        </div>
    );
}