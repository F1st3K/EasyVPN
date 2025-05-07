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
            alignItems="center"
            gap={2}
            flexWrap="wrap"
            width={'100%'}
        >
            <Box display="flex" alignItems="center" gap={1}>
                <Box
                    component="img"
                    src={props.connection.client.icon}
                    sx={{
                        width: '5ch',
                        height: '5ch',
                        borderRadius: '50%',
                        objectFit: 'cover',
                    }}
                />
                {props.connection.client.firstName} {props.connection.client.lastName}
            </Box>
            <KeyboardTab />
            <Box display="flex" alignItems="center" gap={1}>
                <Box
                    component="img"
                    src={props.connection.server.protocol.icon}
                    sx={{
                        width: '5ch',
                        height: '5ch',
                        borderRadius: '50%',
                        objectFit: 'cover',
                    }}
                />
                {props.connection.server.protocol.name}
            </Box>
        </Box>
    );
};

export default ConnectionItem;
