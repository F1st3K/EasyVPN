import { makeAutoObservable } from "mobx";
import EasyVpn, { Auth, Register, Role, User } from "../api";

export default class AuthStore {
    private readonly tokenName = "token";
    user = {} as User;
    roles = [] as Role[];
    isAuth = false;

    constructor() {
        makeAutoObservable(this);
    }

    async register(info: Register) {
        var auth = await EasyVpn.auth.register(info)
            .then(r => r.data);
        localStorage.setItem(this.tokenName, auth.token);
        this.setAuth(auth);
    }

    async login(username: string, password: string) {
        var auth = await EasyVpn.auth.login(username, password)
            .then(r => r.data);
        localStorage.setItem(this.tokenName, auth.token);
        this.setAuth(auth);
    }

    async logout() {
        localStorage.removeItem(this.tokenName);
        this.resetAuth();
    }

    async checkAuth() {
        this.resetAuth();

        var token = localStorage.getItem(this.tokenName);
        if (token === null)
            return;
        
        var auth = await EasyVpn.auth.check(token)
            .then(r => r.data);

        localStorage.setItem(this.tokenName, auth.token);
        this.setAuth(auth);
    }

    private setAuth(auth: Auth) {
        this.user = auth;
        this.roles = auth.roles;
        this.isAuth = true;
    }

    private resetAuth() {
        this.user = {} as User;
        this.roles = [] as Role[];
        this.isAuth = false;
    }
}