import { LinearProgress } from '@mui/material';
import { HttpStatusCode } from 'axios';
import { observer } from 'mobx-react-lite';
import React from 'react';
import { FC, ReactElement, useContext } from 'react';

import { Context } from '..';
import { ApiError } from '../api';
import { useRequest } from '../hooks';

interface AuthProviderProps {
    children?: ReactElement;
}

const AuthProvider: FC<AuthProviderProps> = (props: AuthProviderProps) => {
    const store = useContext(Context);
    const [, loading, error] = useRequest<void, ApiError>(() => store.Auth.checkAuth());

    if (loading) return <LinearProgress />;

    if (error?.response?.data.status === HttpStatusCode.Unauthorized) store.Auth.logout();

    return <>{props.children}</>;
};

export default observer(AuthProvider);
