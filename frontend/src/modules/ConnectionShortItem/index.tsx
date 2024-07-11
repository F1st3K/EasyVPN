import { ContentPasteSearch, DeleteForever, PostAdd } from '@mui/icons-material';
import { Avatar, Box, Chip, IconButton, ListItemAvatar, ListItemText } from '@mui/material';
import React, { FC } from 'react';

import { Connection } from '../../api';

const ConnectionShortItem: FC<Connection> = (connection: Connection) => {
    const expt = new Date(connection.validUntil);
    const wart = new Date(new Date(expt).setDate(expt.getDate() - 3));
    return (
        <Box display="flex" flexDirection="row" flexWrap="wrap" alignItems="center" gap={2} width={'100%'}>
            <Box display="flex" flexDirection="row" alignItems="center">
                <ListItemAvatar>
                    <Avatar src={connection.server.protocol.icon} />
                </ListItemAvatar>
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
            </Box>
            <Box display="flex" justifyContent="space-between" alignItems="center" flex="1">
                <Box display="flex" gap={1}>
                    <Chip
                        label="Config"
                        icon={<ContentPasteSearch />}
                        color="info"
                        onClick={() => console.log('Get config clicked')}
                    />
                    <Chip
                        variant="filled"
                        label="Extend"
                        icon={<PostAdd />}
                        color="success"
                        onClick={() => console.log('Extend clicked')}
                    />
                </Box>
                <IconButton
                    aria-label="delete"
                    sx={{
                        visibility: new Date() > expt ? 'visible' : 'hidden',
                        marginX: 1,
                    }}
                    color="error"
                    onClick={() => console.log('Delete clicked')}
                >
                    <DeleteForever />
                </IconButton>
            </Box>
        </Box>
    );
};

export default ConnectionShortItem;
