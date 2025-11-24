import { HttpStatusCode } from 'axios';
import { makeAutoObservable } from 'mobx';

import EasyZsv, { ApiError, Auth, Register, Role, User } from '../api';

export default class AuthStore {
    private readonly tokenName = 'token';
    user = {} as User;
    roles = [] as Role[];
    isAuth = false;

    constructor() {
        makeAutoObservable(this);
    }

    public getToken() {
        return localStorage.getItem(this.tokenName) ?? '';
    }

    public async register(info: Register) {
        await EasyZsv.auth.register(info).then((r) => this.setAuth(r.data));
    }

    public async login(username: string, password: string) {
        await EasyZsv.auth.login(username, password).then((r) => this.setAuth(r.data));
    }

    public async checkAuth() {
        const token = localStorage.getItem(this.tokenName);
        if (token === null) {
            this.logout();
            return;
        }

        await EasyZsv.auth
            .check(token)
            .then((r) => this.setAuth(r.data))
            .catch((e: ApiError) => {
                if (e?.status === HttpStatusCode.Unauthorized) this.logout();
                else throw e;
            });
    }

    public logout() {
        localStorage.removeItem(this.tokenName);
        this.user = {} as User;
        this.roles = [] as Role[];
        this.isAuth = false;
    }

    private setAuth(auth: Auth) {
        localStorage.setItem(this.tokenName, auth.token);
        this.user = auth;
        this.roles = auth.roles;
        this.isAuth = true;
    }
}
