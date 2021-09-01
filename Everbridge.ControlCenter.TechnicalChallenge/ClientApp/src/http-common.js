"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var axios_1 = require("axios");
exports.default = axios_1.default.create({
    baseURL: "https://localhost:12443/api",
    headers: {
        "Content-type": "application/json"
    }
});
//# sourceMappingURL=http-common.js.map