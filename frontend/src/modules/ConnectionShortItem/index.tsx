import { ContentPasteSearch, DeleteForever, PostAdd } from '@mui/icons-material';
import {
    Avatar,
    Box,
    Chip,
    IconButton,
    ListItemAvatar,
    ListItemText,
} from '@mui/material';
import React, { FC, useState } from 'react';

import { Connection } from '../../api';
import Modal from '../../components/Modal';
import ConfigModal from '../ConfigModal';

interface ConnectionShortItemProps {
    connection: Connection;
}

const ConnectionShortItem: FC<ConnectionShortItemProps> = (props) => {
    const [isConfig, setIsConfig] = useState(false);
    const [isExtend, setIsExtend] = useState(false);
    const [isDelete, setIsDelete] = useState(false);

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
                                    {new Date() > expt ? (
                                        <>Expired on</>
                                    ) : (
                                        <>Expires from</>
                                    )}{' '}
                                    {expt.toDateString()} at{' '}
                                    {expt.getHours().toString().padStart(2, '0')}:
                                    {expt.getMinutes().toString().padStart(2, '0')}
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
                        onClick={() => setIsConfig(true)}
                    />
                    <Chip
                        variant="filled"
                        label="Extend"
                        icon={<PostAdd />}
                        color="success"
                        onClick={() => setIsExtend(true)}
                    />
                </Box>
                <IconButton
                    aria-label="delete"
                    sx={{
                        visibility: new Date() > expt ? 'visible' : 'hidden',
                        marginX: 1,
                    }}
                    color="error"
                    onClick={() => setIsDelete(true)}
                >
                    <DeleteForever />
                </IconButton>
            </Box>
            <ConfigModal
                open={isConfig}
                handleClose={() => setIsConfig(false)}
                connectionId={props.connection.id}
            />
            <Modal open={isExtend} handleClose={() => setIsExtend(false)} />
            <Modal open={isDelete} handleClose={() => setIsDelete(false)} />
        </Box>
    );
};

export default ConnectionShortItem;
