import { Box, Chip, ListItem, ListItemText } from '@mui/material';
import React, { FC } from 'react';

import { Connection } from '../../api';

const ConnectionShortItem: FC<Connection> = (connection: Connection) => {
    const expt = new Date(connection.validUntil);
    const wart = new Date(new Date(expt).setDate(expt.getDate() - 3));
    return (
        <Box display="flex" flexDirection="row" flexWrap={'wrap'} gap={3}>
            <ListItemText
                primary={connection.server.protocol.name}
                secondary={
                    <Chip
                        label={
                            <>
                                {new Date() > expt ? <>Expired on</> : <>Expires from</>} {expt.toDateString()} at{' '}
                                {expt.getHours().toString().padStart(2, '0')}:
                                {expt.getMinutes().toString().padStart(2, '0')}
                            </>
                        }
                        variant="outlined"
                        color={new Date() > wart ? (new Date() > expt ? 'error' : 'warning') : 'info'}
                        size="small"
                    />
                }
            />
            <Box display="flex" justifyContent="space-between" alignItems="center" gap={3}>
                <Box display="flex" gap={1}>
                    <Chip label="Get config" color="info" onClick={() => console.log('Get config clicked')} />
                    <Chip label="Extend" color="success" onClick={() => console.log('Extend clicked')} />
                </Box>
                <Chip label="Delete" color="error" onClick={() => console.log('Delete clicked')} />
            </Box>
        </Box>
    );
};

export default ConnectionShortItem;
