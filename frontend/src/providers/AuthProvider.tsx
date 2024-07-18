import { LinearProgress } from '@mui/material';
import {} from 'axios';
import { observer } from 'mobx-react-lite';
import React from 'react';
import { FC, ReactElement, useContext } from 'react';
import { useLocation } from 'react-router-dom';

import { Context } from '..';
import config from '../config.json';
import { useIntervalCounter, useRequest } from '../hooks';

interface AuthProviderProps {
    children?: ReactElement;
}

const AuthProvider: FC<AuthProviderProps> = (props: AuthProviderProps) => {
    const counter = useIntervalCounter(config.AuthCheckMinutes);
    const location = useLocation();

    const store = useContext(Context);
    const [, loading] = useRequest<void>(
        () => store.Auth.checkAuth(),
        [counter, location],
    );

    if (loading) return <LinearProgress />;

    return <>{props.children}</>;
};

export default observer(AuthProvider);
