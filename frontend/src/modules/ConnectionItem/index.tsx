import { ListItem, ListItemText } from '@mui/material';
import React, { FC } from 'react';

import { Connection } from '../../api';

const ConnectionItem: FC<Connection> = (connection: Connection) => {
    return (
        <ListItem>
            <ListItemText
                primary={connection.server.protocol.name}
                secondary={
                    <>
                        Expires from {new Date(connection.validUntil).toDateString()} at{' '}
                        {new Date(connection.validUntil).toTimeString()}
                    </>
                }
            />
        </ListItem>
    );
};

export default ConnectionItem;
