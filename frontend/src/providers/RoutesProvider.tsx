import { observer } from 'mobx-react-lite';
import React from 'react';
import { FC, ReactElement, useContext } from 'react';
import { Navigate, Route, Routes, useLocation } from 'react-router-dom';

import { Context } from '..';
import { Role } from '../api';
import ConfigModal from '../modules/ConfigModal';
import CreateConnectionModal from '../modules/CreateConnectionModal';
import DeleteConnectionModal from '../modules/DeleteConnectionModal';
import ExtendConnectionModal from '../modules/ExtendConnectionModal';
import AuthPage from '../pages/AuthPage';
import ClientConnectionsPage from '../pages/ClientConnectionsPage';
import ForbiddenPage from '../pages/ForbiddenPage';
import MainPage from '../pages/MainPage';
import NotFoundPage from '../pages/NotFoundPage';
import PaymentTicketsPage from '../pages/PaymentTicketsPage';
import ProfilePage from '../pages/ProfilePage';
import Root from '../pages/Root';

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
                <Route index element={<MainPage />} />
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
                                with={Role.Administrator}
                                for={<>control connections</>}
                            />
                        }
                    />
                    <Route
                        path="users"
                        element={
                            <Auth with={Role.Administrator} for={<>control users</>} />
                        }
                    />
                    <Route
                        path="servers"
                        element={
                            <Auth with={Role.Administrator} for={<>control servers</>} />
                        }
                    />
                </Route>
                <Route path="tickets/">
                    <Route index element={<Navigate to={'support'} />} />
                    <Route
                        path="support"
                        element={
                            <Auth with={Role.Administrator} for={<>support tickets</>} />
                        }
                    />
                    <Route
                        path="payment"
                        element={
                            <Auth
                                with={Role.PaymentReviewer}
                                for={<PaymentTicketsPage />}
                            />
                        }
                    />
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
                </Route>
                <Route path="*" element={<NotFoundPage />} />
            </Route>
        </Routes>
    );
};

export default RoutesProvider;
