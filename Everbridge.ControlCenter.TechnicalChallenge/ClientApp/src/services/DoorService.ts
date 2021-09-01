import http from "../http-common";
import IDoorData from "../types/Door";

const getAll = () => {
    return http.get("/door");
};

const get = (id: any) => {
    return http.get(`/door/${id}`);
};

const create = (data: IDoorData) => {
    return http.post("/door", data);
};

const update = (id: any, data: IDoorData) => {
    return http.patch(`/door/${id}`, [
        { "op": "replace", "path": "/label", "value": data.label },
        { "op": "replace", "path": "/isOpen", "value": data.isOpen },
        { "op": "replace", "path": "/isLocked", "value": data.isLocked }
    ]);
};

const remove = (id: any) => {
    return http.delete(`/door/${id}`);
};

const removeAll = () => {
    return http.delete(`/door`);
};

const findByTitle = (title: string) => {
    return http.get(`/door?title=${title}`);
};

const DoorService = {
    getAll,
    get,
    create,
    update,
    remove,
    removeAll,
    findByTitle,
};

export default DoorService;