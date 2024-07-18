import { HttpStatusCode } from 'axios';
import { makeAutoObservable } from 'mobx';

import EasyVpn, { Auth, Register, Role, User } from '../api';

export default class AuthStore {
    private readonly tokenName = 'token';
    user = {} as User;
    roles = [] as Role[];
    isAuth = false;

    constructor() {
        makeAutoObservable(this);
    }

    public getToken() {
        return localStorage.getItem(this.tokenName);
    }

    async register(info: Register) {
        const auth = await EasyVpn.auth.register(info).then((r) => r.data);
        localStorage.setItem(this.tokenName, auth.token);
        this.setAuth(auth);
    }

    async login(username: string, password: string) {
        const auth = await EasyVpn.auth.login(username, password).then((r) => r.data);
        localStorage.setItem(this.tokenName, auth.token);
        this.setAuth(auth);
    }

    async logout() {
        localStorage.removeItem(this.tokenName);
        this.user = {} as User;
        this.roles = [] as Role[];
        this.isAuth = false;
    }

    async checkAuth() {
        const token = localStorage.getItem(this.tokenName);
        if (token === null) {
            this.logout();
            return;
        }

        await EasyVpn.auth
            .check(token)
            .then((r) => {
                localStorage.setItem(this.tokenName, r.data.token);
                this.setAuth(r.data);
            })
            .catch((e) => {
                if (e?.response?.data.status === HttpStatusCode.Unauthorized)
                    this.logout();
            });
    }

    private setAuth(auth: Auth) {
        this.user = auth;
        this.roles = auth.roles;
        this.isAuth = true;
    }
}
