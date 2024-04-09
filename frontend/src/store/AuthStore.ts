import { makeAutoObservable } from "mobx";
import User from "../api/types/User";
import EasyVpn from "../api";



export default class AuthStore {
    private readonly tokenName = "token"
    user = {} as User;
    isAuth = false;

    constructor() {
        makeAutoObservable(this);
    }

    async login(username: string, password: string) {
        var auth = (await EasyVpn.auth.login(username, password)).data;
        localStorage.setItem(this.tokenName, auth.token);
        this.user = auth;
        this.isAuth = true;
    }

    async logout() {
        localStorage.removeItem(this.tokenName)
        this.user = {} as User;
        this.isAuth = false;
    }
}