import axios from "axios"
import config from "../config.json"
import AuthResponse from "./types/AuthResponse"
import ApiError from "./types/ApiError"
import User from "./types/User"


const api = axios.create({
    baseURL: config.ApiUrl
})

const EasyVpn = {
    auth: {
        login: (login: string, password: string) => {
            return api.post<AuthResponse>("/auth/login", {
                login,
                password
            })
        },
        check: (token: string) => {
            return api.get<AuthResponse>("/auth/check", {
                headers: {Authorization: `Bearer ${token}`}
            })
        },
        register: (firstName: string, lastName: string, login: string, password: string) => {
            return api.post<AuthResponse>("/auth/register", {
                firstName,
                lastName,
                login,
                password
            })
        }
    }
}

export default EasyVpn

export type {
    ApiError,
    User
}