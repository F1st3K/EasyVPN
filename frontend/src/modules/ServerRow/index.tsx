import { DeleteForever, Edit } from '@mui/icons-material';
import { Chip, IconButton, TableCell, TableRow } from '@mui/material';
import React, { FC } from 'react';

import { Server } from '../../api';

interface ServerRowProps {
    server: Server;
    onEdit?: (id: string) => void;
    onRemove?: (id: string) => void;
}

const ServerRow: FC<ServerRowProps> = (props: ServerRowProps) => {
    return (
        <TableRow hover>
            <TableCell sx={{ padding: '10px' }}>
                <Chip label={props.server.version} color={'secondary'} />
            </TableCell>
            <TableCell sx={{ padding: '10px' }}>
                <img loading="lazy" width={50} src={props.server.protocol.icon} alt="" />
            </TableCell>
            <TableCell
                sx={{
                    padding: '10px',
                    whiteSpace: 'nowrap',
                    overflow: 'hidden',
                    textOverflow: 'ellipsis',
                    maxWidth: 'min(calc(100vw - 100px), 60vw)',
                }}
            >
                {props.server.protocol.name}
            </TableCell>
            <TableCell sx={{ padding: '10px' }}>
                <IconButton
                    sx={{ marginLeft: '5px' }}
                    onClick={() => props.onEdit?.(props.server.id)}
                >
                    <Edit />
                </IconButton>
            </TableCell>
            <TableCell sx={{ padding: '10px' }}>
                <IconButton
                    sx={{ marginLeft: '5px' }}
                    onClick={() => props.onRemove?.(props.server.id)}
                    color="error"
                >
                    <DeleteForever />
                </IconButton>
            </TableCell>
        </TableRow>
    );
};

export default ServerRow;
