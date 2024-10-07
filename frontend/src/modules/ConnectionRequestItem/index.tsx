import React, { FC } from 'react';

import { ApiError, Connection } from '../../api';
import { useRequest } from '../../hooks';
import ConnectionItem from '../ConnectionItem';

interface ConnectionRequestItemProps {
    connectionPromise: () => Promise<Connection>;
}

const ConnectionRequestItem: FC<ConnectionRequestItemProps> = (props) => {
    const [conneciton] = useRequest<Connection, ApiError>(() =>
        props.connectionPromise(),
    );

    if (!conneciton) return <></>;

    return <ConnectionItem connection={conneciton} />;
};

export default ConnectionRequestItem;
