import { observer } from 'mobx-react-lite';
import React from 'react';
import { FC, ReactElement, useContext } from 'react';
import { Navigate, Route, Routes, useLocation } from 'react-router-dom';

import { Context } from '..';
import { Role } from '../api';
import ClientTicketModal from '../modules/ClientTicketModal';
import ConfigModal from '../modules/ConfigModal';
import ConfigureServerModal from '../modules/ConfigureServerModal';
import CreateConnectionModal from '../modules/CreateConnectionModal';
import CreateProtocolModal from '../modules/CreateProtolModal';
import CreateShortConnectionModal from '../modules/CreateShortConnectionModal';
import DeleteConnectionModal from '../modules/DeleteConnectionModal';
import EditProtocolModal from '../modules/EditProtocolModal';
import ExtendConnectionModal from '../modules/ExtendConnectionModal';
import PaymentTikcetModal from '../modules/PaymentTikcetModal';
import SetupServerModal from '../modules/SetupServerModal';
import AuthPage from '../pages/AuthPage';
import ClientConnectionsPage from '../pages/ClientConnectionsPage';
import ConnectionsControlPage from '../pages/ConnectionsControlPage';
import CreateDynamicPage from '../pages/CreateDynamicPage';
import DynamicPages from '../pages/DynamicPages';
import ForbiddenPage from '../pages/ForbiddenPage';
import NotFoundPage from '../pages/NotFoundPage';
import PaymentTicketsPage from '../pages/PaymentTicketsPage';
import ProfilePage from '../pages/ProfilePage';
import ProtocolsControlPage from '../pages/ProtocolsControlPage';
import Root from '../pages/Root';
import ServersControlPage from '../pages/ServersControlPage';
import UsersControlPage from '../pages/UsersControlPage';

interface ForProps {
    for: ReactElement;
}

interface ForWithProps<T> extends ForProps {
    with?: T;
}

const Unauth: FC<ForProps> = observer((props: ForProps) => {
    const { Auth } = useContext(Context);

    if (Auth.isAuth) return <Navigate to={'/'} />;

    return <>{props.for}</>;
});

const Auth: FC<ForWithProps<Role>> = observer((props: ForWithProps<Role>) => {
    const { Auth } = useContext(Context);
    const location = useLocation();

    if (Auth.isAuth === false)
        return (
            <Navigate
                to={`/auth?prevPage=${location.pathname}${location.search}${location.hash}`}
            />
        );

    if (props.with && Auth.roles.includes(props.with) === false) return <ForbiddenPage />;

    return <>{props.for}</>;
});

const Redirect = (props: { to: string }) => {
    const { search, hash } = useLocation();

    return <Navigate to={`${props.to}${search}${hash}`} />;
};

const RoutesProvider: FC = () => {
    return (
        <Routes>
            <Route path="/" element={<Root />}>
                <Route index element={<Redirect to="pages/" />} />
                <Route path="pages/">
                    <Route index element={<Redirect to="main" />} />
                    <Route
                        path="new"
                        element={
                            <Auth with={Role.PageModerator} for={<CreateDynamicPage />} />
                        }
                    />
                    <Route path="*" element={<DynamicPages />} />
                </Route>
                <Route path="auth/">
                    <Route index element={<Redirect to="login" />} />
                    <Route
                        path="login"
                        element={<Unauth for={<AuthPage tab="login" />} />}
                    />
                    <Route
                        path="register"
                        element={<Unauth for={<AuthPage tab="register" />} />}
                    />
                </Route>
                <Route path="control/">
                    <Route index element={<Navigate to={'connections'} />} />
                    <Route
                        path="connections"
                        element={
                            <Auth
                                with={Role.ConnectionRegulator}
                                for={<ConnectionsControlPage />}
                            />
                        }
                    >
                        <Route path="new" element={<CreateShortConnectionModal />} />
                        <Route path=":connectionId/config" element={<ConfigModal />} />
                    </Route>
                    <Route
                        path="users"
                        element={
                            <Auth with={Role.SecurityKeeper} for={<UsersControlPage />} />
                        }
                    />
                    <Route
                        path="servers"
                        element={
                            <Auth
                                with={Role.ServerSetuper}
                                for={<ServersControlPage />}
                            />
                        }
                    >
                        <Route path="new" element={<SetupServerModal />} />
                        <Route path=":serverId" element={<ConfigureServerModal />} />
                    </Route>
                    <Route
                        path="protocols"
                        element={
                            <Auth
                                with={Role.ServerSetuper}
                                for={<ProtocolsControlPage />}
                            />
                        }
                    >
                        <Route path="new" element={<CreateProtocolModal />} />
                        <Route path=":protocolId" element={<EditProtocolModal />} />
                    </Route>
                </Route>
                <Route path="tickets/">
                    <Route
                        path="payment"
                        element={
                            <Auth
                                with={Role.PaymentReviewer}
                                for={<PaymentTicketsPage />}
                            />
                        }
                    >
                        <Route path=":ticketId" element={<PaymentTikcetModal />} />
                    </Route>
                </Route>
                <Route path="profile" element={<Auth for={<ProfilePage />} />} />
                <Route
                    path="connections/"
                    element={<Auth with={Role.Client} for={<ClientConnectionsPage />} />}
                >
                    <Route path="new" element={<CreateConnectionModal />} />
                    <Route path=":connectionId/config" element={<ConfigModal />} />
                    <Route
                        path=":connectionId/extend"
                        element={<ExtendConnectionModal />}
                    />
                    <Route
                        path=":connectionId/delete"
                        element={<DeleteConnectionModal />}
                    />
                    <Route path="ticket/:ticketId" element={<ClientTicketModal />} />
                </Route>
                <Route path="*" element={<NotFoundPage />} />
            </Route>
        </Routes>
    );
};

export default RoutesProvider;
