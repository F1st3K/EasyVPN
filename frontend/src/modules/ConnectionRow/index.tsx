import { TableCell, TableRow, Typography } from '@mui/material';
import React, { FC, useState } from 'react';

import { Connection } from '../../api';

interface ConnectionRowProps {
    connection: Connection;
    onExtend?: (id: string, days: number) => void;
    onReset?: (id: string) => void;
}

const ConnectionRow: FC<ConnectionRowProps> = (props: ConnectionRowProps) => {
    const [days, setDays] = useState<string>();

    return (
        <TableRow hover>
            <TableRow>
                <TableCell
                    sx={{
                        padding: '10px',
                        whiteSpace: 'nowrap',
                        overflow: 'hidden',
                        textOverflow: 'ellipsis',
                        maxWidth: 'min(calc(50vw - 50px), 25vw)',
                    }}
                >
                    <Typography variant="caption">
                        {props.connection.client.firstName}{' '}
                        {props.connection.client.lastName}
                    </Typography>
                </TableCell>
            </TableRow>
        </TableRow>
    );
};

export default ConnectionRow;
