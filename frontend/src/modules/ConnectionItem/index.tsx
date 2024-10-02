import { KeyboardTab } from '@mui/icons-material';
import { Box } from '@mui/material';
import React, { FC } from 'react';

import { Connection } from '../../api';

interface ConnectionItemProps {
    connection: Connection;
}

const ConnectionItem: FC<ConnectionItemProps> = (props) => {
    return (
        <Box
            display="flex"
            flexDirection="row"
            flexWrap="wrap"
            alignItems="center"
            gap={2}
            width={'100%'}
        >
            {props.connection.client.firstName} {props.connection.client.lastName}
            <KeyboardTab />
            <img
                loading="lazy"
                width={25}
                src={props.connection.server.protocol.icon}
                alt=""
            />
            {props.connection.server.protocol.name}
        </Box>
    );
};

export default ConnectionItem;
