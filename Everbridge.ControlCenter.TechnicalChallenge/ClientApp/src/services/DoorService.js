"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var http_common_1 = require("../http-common");
var getAll = function () {
    return http_common_1.default.get("/door");
};
var get = function (id) {
    return http_common_1.default.get("/door/" + id);
};
var create = function (data) {
    return http_common_1.default.post("/door", data);
};
var update = function (id, data) {
    return http_common_1.default.patch("/door/" + id, [
        { "op": "replace", "path": "/label", "value": data.label },
        { "op": "replace", "path": "/isOpen", "value": data.isOpen },
        { "op": "replace", "path": "/isLocked", "value": data.isLocked }
    ]);
};
var remove = function (id) {
    return http_common_1.default.delete("/door/" + id);
};
var removeAll = function () {
    return http_common_1.default.delete("/door");
};
var findByTitle = function (title) {
    return http_common_1.default.get("/door?title=" + title);
};
var DoorService = {
    getAll: getAll,
    get: get,
    create: create,
    update: update,
    remove: remove,
    removeAll: removeAll,
    findByTitle: findByTitle,
};
exports.default = DoorService;
//# sourceMappingURL=DoorService.js.map