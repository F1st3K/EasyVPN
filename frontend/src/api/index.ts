import axios from 'axios';

import config from '../config.json';
import { ConnectionTicketStatus } from './enums/ConnectionTicketStatus';
import { Role } from './enums/Role';
import { VpnVersion } from './enums/VpnVersion';
import Register from './requests/Register';
import ApiError from './responses/ApiError';
import Auth from './responses/Auth';
import Connection from './responses/Connection';
import ConnectionConfig from './responses/ConnectionConfig';
import ConnectionTicket from './responses/ConnectionTicket';
import Protocol from './responses/Protocol';
import Server from './responses/Server';
import User from './responses/User';

const api = axios.create({
    baseURL: config.ApiUrl,
});

const EasyVpn = {
    auth: {
        login: (login: string, password: string) => {
            return api.post<Auth>(`/auth/login`, {
                login,
                password,
            });
        },
        check: (token: string) => {
            return api.get<Auth>(`/auth/check`, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        register: (request: Register) => {
            return api.post<Auth>(`/auth/register`, request);
        },
    },
    my: {
        connections: (token: string) => {
            return api.get<Connection[]>(`/my/connections`, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        configConnection: (connectionId: string, token: string) => {
            return api.get<ConnectionConfig>(`/my/connections/${connectionId}/config`, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        tickets: (token: string) => {
            return api.get<ConnectionTicket[]>(`/my/tickets`, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
    },
};

export default EasyVpn;

export type {
    ApiError,
    Auth,
    Connection,
    ConnectionConfig,
    ConnectionTicket,
    Protocol,
    Server,
    User,
};

export type { Register };

export { ConnectionTicketStatus, Role, VpnVersion };
