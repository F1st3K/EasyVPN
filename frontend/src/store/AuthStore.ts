import { makeAutoObservable } from "mobx";
import User from "../api/types/User";
import EasyVpn from "../api";
import { Role } from "../api/types/Roles";

export default class AuthStore {
    private readonly tokenName = "token";
    user = {} as User;
    roles = [] as Role[];
    isAuth = false;

    constructor() {
        makeAutoObservable(this);
    }

    async login(username: string, password: string) {
        var auth = (await EasyVpn.auth.login(username, password)).data;
        localStorage.setItem(this.tokenName, auth.token);
        this.user = auth;
        this.roles = auth.roles;
        this.isAuth = true;
    }

    async logout() {
        localStorage.removeItem(this.tokenName)
        this.user = {} as User;
        this.roles = [] as Role[];
        this.isAuth = false;
    }

    async checkAuth() {
        this.user = {} as User;
        this.roles = [] as Role[];
        this.isAuth = false;

        var token = localStorage.getItem(this.tokenName);
        if (token === null)
            return;

        var auth = (await EasyVpn.auth.check(token)).data;
        
        localStorage.setItem(this.tokenName, auth.token);
        this.user = auth;
        this.roles = auth.roles;
        this.isAuth = true;
    }

}