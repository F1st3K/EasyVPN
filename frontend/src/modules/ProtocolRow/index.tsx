import { DeleteForever, Edit } from '@mui/icons-material';
import { IconButton, TableCell, TableRow } from '@mui/material';
import React, { FC } from 'react';

import { Protocol } from '../../api';

interface ProtocolRowProps {
    protocol: Protocol;
    onEdit?: (id: string) => void;
    onRemove?: (id: string) => void;
}

const ProtocolRow: FC<ProtocolRowProps> = (props: ProtocolRowProps) => {
    return (
        <TableRow hover>
            <TableCell sx={{ padding: '10px' }}>
                <img loading="lazy" width={50} src={props.protocol.icon} alt="" />
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
                {props.protocol.name}
            </TableCell>
            <TableCell sx={{ padding: '10px' }}>
                <IconButton
                    sx={{ marginLeft: '5px' }}
                    onClick={() => props.onEdit?.(props.protocol.id)}
                >
                    <Edit />
                </IconButton>
            </TableCell>
            <TableCell sx={{ padding: '10px' }}>
                <IconButton
                    sx={{ marginLeft: '5px' }}
                    onClick={() => props.onRemove?.(props.protocol.id)}
                    color="error"
                >
                    <DeleteForever />
                </IconButton>
            </TableCell>
        </TableRow>
    );
};

export default ProtocolRow;
