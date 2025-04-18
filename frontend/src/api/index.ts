import axios from 'axios';

import config from '../config';
import PaymentConnectionInfo from './common/PaymentConnectionInfo';
import { ConnectionTicketStatus } from './enums/ConnectionTicketStatus';
import { Role } from './enums/Role';
import { VpnVersion } from './enums/VpnVersion';
import CreateConnection from './requests/CreateConnection';
import ExtendConnection from './requests/ExtendConnection';
import Page from './requests/Page';
import Register from './requests/Register';
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
        connection: (connectionId: string, token: string) => {
            return api.get<Connection>(`/connections/${connectionId}`, {
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
    servers: {
        getAll: (token: string) => {
            return api.get<Server[]>(`/servers`, {
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
};

export default EasyVpn;

export type { PaymentConnectionInfo };

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
