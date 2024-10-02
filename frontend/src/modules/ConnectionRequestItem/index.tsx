import { Alert, CircularProgress } from '@mui/material';
import React, { FC, useContext } from 'react';

import { Context } from '../..';
import EasyVpn, { ApiError, Connection } from '../../api';
import { useRequest } from '../../hooks';
import ConnectionItem from '../ConnectionItem';

interface ConnectionRequestItemProps {
    connecitonId: string;
}

const ConnectionRequestItem: FC<ConnectionRequestItemProps> = (props) => {
    const { Auth } = useContext(Context);

    const [conneciton, loading, error] = useRequest<Connection, ApiError>(() =>
        EasyVpn.payment
            .connection(props.connecitonId, Auth.getToken())
            .then((v) => v.data),
    );

    if (loading || !conneciton) return <CircularProgress />;

    if (error)
        return (
            <Alert severity="error" variant="outlined" sx={{ width: '25ch' }}>
                {error.response?.data.title ?? error.message}
            </Alert>
        );

    return <ConnectionItem connection={conneciton} />;
};

export default ConnectionRequestItem;
