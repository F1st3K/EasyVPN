import axios from 'axios';

import config from '../config';
import PaymentConnectionInfo from './common/PaymentConnectionInfo';
import { ConnectionTicketStatus } from './enums/ConnectionTicketStatus';
import { Role } from './enums/Role';
import { VpnVersion } from './enums/VpnVersion';
import ConnectionString from './requests/ConnectionString';
import CreateConnection from './requests/CreateConnection';
import ExtendConnection from './requests/ExtendConnection';
import Page from './requests/Page';
import ProtocolInfo from './requests/ProtocolInfo';
import Register from './requests/Register';
import ServerInfo from './requests/ServerInfo';
import UserInfo from './requests/UserInfo';
import ApiError from './responses/ApiError';
import Auth from './responses/Auth';
import Connection from './responses/Connection';
import ConnectionConfig from './responses/ConnectionConfig';
import ConnectionTicket from './responses/ConnectionTicket';
import PageInfo from './responses/PageInfo';
import PageWithDates from './responses/PageWithDates';
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
        deleteConnection: (connectionId: string, token: string) => {
            return api.delete<void>(`/my/connections/${connectionId}`, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        createConnection: (request: CreateConnection, token: string) => {
            return api.post<void>(`/my/connections`, request, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        extendConnection: (request: ExtendConnection, token: string) => {
            return api.post<void>(`/my/connections/extend`, request, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        tickets: (token: string) => {
            return api.get<ConnectionTicket[]>(`/my/tickets`, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        ticket: (ticketId: string, token: string) => {
            return api.get<ConnectionTicket>(`/my/tickets/${ticketId}`, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        connection: (connectionId: string, token: string) => {
            return api.get<Connection>(`/my/connections/${connectionId}`, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        profile: (token: string) => {
            return api.get<User>(`/my/profile`, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        editProfile: (request: UserInfo, token: string) => {
            return api.put<void>(`/my/profile`, request, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        updateLogin: (login: string, token: string) => {
            return api.put<void>(`/my/profile/login`, login, {
                headers: {
                    Authorization: `Bearer ${token}`,
                    'Content-Type': 'application/json',
                },
            });
        },
        updatePassword: (pwd: string, newPwd: string, token: string) => {
            return api.put<void>(
                `/my/profile/password`,
                { password: pwd, newPassword: newPwd },
                {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                },
            );
        },
    },
    payment: {
        tickets: (token: string, clientId?: string) => {
            return api.get<ConnectionTicket[]>(
                `/payment/tickets${clientId ? '?clientId=' + clientId : ''}`,
                {
                    headers: { Authorization: `Bearer ${token}` },
                },
            );
        },
        ticket: (ticketId: string, token: string) => {
            return api.get<ConnectionTicket>(`/payment/tickets/${ticketId}`, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        confirm: (ticketId: string, token: string, days?: number) => {
            return api.put<void>(
                `/payment/tickets/${ticketId}/confirm${days ? '?days=' + days : ''}`,
                undefined,
                {
                    headers: { Authorization: `Bearer ${token}` },
                },
            );
        },
        reject: (ticketId: string, token: string) => {
            return api.put<void>(`/payment/tickets/${ticketId}/reject`, undefined, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
    },
    connections: {
        get: (id: string, token: string) => {
            return api.get<Connection>(`/connections/${id}`, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        getAll: (token: string) => {
            return api.get<Connection[]>(`/connections`, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        getConfig: (id: string, token: string) => {
            return api.get<ConnectionConfig>(`/connections/${id}/config`, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        create: (serverId: string, clientId: string, token: string) => {
            return api.post<void>(
                `/connections?serverId=${serverId}&clientId=${clientId}`,
                undefined,
                { headers: { Authorization: `Bearer ${token}` } },
            );
        },
        reset: (connectionId: string, token: string) => {
            return api.put<void>(`/connections/${connectionId}/reset`, undefined, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        extend: (connectionId: string, days: number, token: string) => {
            return api.put<void>(
                `/connections/${connectionId}/extend?days=${days}`,
                undefined,
                {
                    headers: { Authorization: `Bearer ${token}` },
                },
            );
        },
    },
    servers: {
        test: (v: VpnVersion, request: ConnectionString, token: string) => {
            return api.post<void>(`/servers/test/${v}`, request, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        getAll: (token: string) => {
            return api.get<Server[]>(`/servers`, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        get: (id: string, token: string) => {
            return api.get<Server>(`/servers/${id}`, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        create: (request: ServerInfo, token: string) => {
            return api.post<void>(`/servers`, request, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        edit: (id: string, request: ServerInfo, token: string) => {
            return api.put<void>(`/servers/${id}`, request, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        delete: (id: string, token: string) => {
            return api.delete<void>(`/servers/${id}`, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
    },
    protocols: {
        getAll: (token: string) => {
            return api.get<Protocol[]>(`/protocols`, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        get: (id: string, token: string) => {
            return api.get<Protocol>(`/protocols/${id}`, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        create: (request: ProtocolInfo, token: string) => {
            return api.post<void>(`/protocols`, request, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        edit: (id: string, request: ProtocolInfo, token: string) => {
            return api.put<void>(`/protocols/${id}`, request, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        delete: (id: string, token: string) => {
            return api.delete<void>(`/protocols/${id}`, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
    },
    admin: {
        connection: (connectionId: string, token: string) => {
            return api.get<Connection>(`/connections/${connectionId}`, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
    },
    pages: {
        getAll: () => {
            return api.get<PageInfo[]>(`/dynamic-pages`);
        },
        get: (route: string) => {
            return api.get<PageWithDates>(`/dynamic-pages/${route}`);
        },
        create: (request: Page, token: string) => {
            return api.post<void>(`/dynamic-pages`, request, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        update: (route: string, request: Page, token: string) => {
            return api.put<void>(`/dynamic-pages/${route}`, request, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        delete: (route: string, token: string) => {
            return api.delete<void>(`/dynamic-pages/${route}`, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
    },
    users: {
        getAll: (token: string) => {
            return api.get<User[]>(`/users`, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        getClients: (token: string) => {
            return api.get<User[]>(`/clients`, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        get: (id: string, token: string) => {
            return api.get<User>(`/users/${id}`, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        updateRoles: (roles: Role[], id: string, token: string) => {
            return api.put<void>(`/users/${id}/roles`, roles, {
                headers: { Authorization: `Bearer ${token}` },
            });
        },
        updatePassword: (pwd: string, id: string, token: string) => {
            return api.put<void>(`/users/${id}/password`, pwd, {
                headers: {
                    Authorization: `Bearer ${token}`,
                    'Content-Type': 'application/json',
                },
            });
        },
    },
};

export default EasyVpn;

export type { PaymentConnectionInfo, ProtocolInfo, ServerInfo, UserInfo };

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

export type { CreateConnection, ExtendConnection, Register };

export { ConnectionTicketStatus, Role, VpnVersion };
