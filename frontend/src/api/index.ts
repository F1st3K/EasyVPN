import axios from "axios"
import config from "../config.json"
import Register from "./requests/Register"
import Auth from "./responses/Auth"
import ApiError from "./responses/ApiError"
import { Role } from "./responses/Roles"
import User from "./responses/User"


const api = axios.create({
    baseURL: config.ApiUrl
})

const EasyVpn = {
    auth: {
        login: (login: string, password: string) => {
            return api.post<Auth>("/auth/login", {
                login,
                password
            })
        },
        check: (token: string) => {
            return api.get<Auth>("/auth/check", {
                headers: {Authorization: `Bearer ${token}`}
            })
        },
        register: (request: Register) => {
            return api.post<Auth>("/auth/register", request)
        }
    }
}

export default EasyVpn

export type {
    ApiError,
    Auth,
    Role,
    User
}

export type {
    Register
}