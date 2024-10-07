import { Alert, CircularProgress } from '@mui/material';
import React, { FC } from 'react';

import { ApiError, Connection } from '../../api';
import { useRequest } from '../../hooks';
import ConnectionItem from '../ConnectionItem';

interface ConnectionRequestItemProps {
    connectionPromise: () => Promise<Connection>;
}

const ConnectionRequestItem: FC<ConnectionRequestItemProps> = (props) => {
    const [conneciton, loading, error] = useRequest<Connection, ApiError>(() =>
        props.connectionPromise(),
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
