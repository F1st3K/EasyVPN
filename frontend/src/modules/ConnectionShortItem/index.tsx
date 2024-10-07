import { ContentPasteSearch, DeleteForever, PostAdd } from '@mui/icons-material';
import {
    Avatar,
    Box,
    Chip,
    IconButton,
    ListItemAvatar,
    ListItemText,
} from '@mui/material';
import { format } from 'date-fns';
import React, { FC } from 'react';
import { useNavigate } from 'react-router-dom';

import { Connection } from '../../api';

interface ConnectionShortItemProps {
    connection: Connection;
}

const ConnectionShortItem: FC<ConnectionShortItemProps> = (props) => {
    const navigate = useNavigate();

    const expt = new Date(props.connection.validUntil);
    const wart = new Date(new Date(expt).setDate(expt.getDate() - 3));
    return (
        <Box
            display="flex"
            flexDirection="row"
            flexWrap="wrap"
            alignItems="center"
            gap={2}
            width={'100%'}
        >
            <Box display="flex" flexDirection="row" alignItems="center">
                <ListItemAvatar>
                    <Avatar src={props.connection.server.protocol.icon} />
                </ListItemAvatar>
                <ListItemText
                    primary={props.connection.server.protocol.name}
                    secondary={
                        <Chip
                            label={
                                <>
                                    {new Date() > expt ? <>Expired</> : <>Expires</>}{' '}
                                    {format(expt, 'dd.MM.yyyy') +
                                        ' at ' +
                                        format(expt, 'HH:mm')}
                                </>
                            }
                            variant="outlined"
                            color={
                                new Date() > wart
                                    ? new Date() > expt
                                        ? 'error'
                                        : 'warning'
                                    : 'info'
                            }
                            size="small"
                        />
                    }
                />
            </Box>
            <Box
                display="flex"
                justifyContent="space-between"
                alignItems="center"
                flex="1"
            >
                <Box display="flex" gap={1}>
                    <Chip
                        label="Config"
                        icon={<ContentPasteSearch />}
                        color="info"
                        onClick={() => navigate(`${props.connection.id}/config`)}
                    />
                    <Chip
                        variant="filled"
                        label="Extend"
                        icon={<PostAdd />}
                        color="success"
                        onClick={() => navigate(`${props.connection.id}/extend`)}
                    />
                </Box>
                <IconButton
                    aria-label="delete"
                    sx={{
                        visibility: new Date() > expt ? 'visible' : 'hidden',
                        marginX: 1,
                    }}
                    color="error"
                    onClick={() => navigate(`${props.connection.id}/delete`)}
                >
                    <DeleteForever />
                </IconButton>
            </Box>
        </Box>
    );
};

export default ConnectionShortItem;
