import { ContentPasteSearch, DeleteForever, PostAdd } from '@mui/icons-material';
import {
    Avatar,
    Box,
    Chip,
    IconButton,
    ListItemAvatar,
    ListItemText,
} from '@mui/material';
import React, { FC } from 'react';
import { useNavigate } from 'react-router-dom';

import { Connection } from '../../api';
import ExpireItem from '../ExpireItem';

interface ConnectionShortItemProps {
    connection: Connection;
}

const ConnectionShortItem: FC<ConnectionShortItemProps> = (props) => {
    const navigate = useNavigate();

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
                        <ExpireItem
                            ExpireTime={new Date(props.connection.validUntil)}
                            WarrningDaysBefore={3}
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
                        visibility:
                            new Date() > new Date(props.connection.validUntil)
                                ? 'visible'
                                : 'hidden',
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
