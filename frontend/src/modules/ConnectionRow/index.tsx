import { ContentPasteSearch, MoreTime, Restore } from '@mui/icons-material';
import { Box, IconButton, TableCell, TableRow, TextField } from '@mui/material';
import React, { FC, useState } from 'react';

import { Connection } from '../../api';
import ConnectionItem from '../ConnectionItem';
import ExpireItem from '../ExpireItem';

interface ConnectionRowProps {
    connection: Connection;
    onConfig?: (id: string) => void;
    onExtend?: (id: string, days: number) => void;
    onReset?: (id: string) => void;
}

const ConnectionRow: FC<ConnectionRowProps> = (props: ConnectionRowProps) => {
    const [days, setDays] = useState<number>();

    return (
        <TableRow hover>
            <TableRow>
                <TableCell
                    sx={{
                        padding: '10px',
                        whiteSpace: 'nowrap',
                        overflow: 'hidden',
                        textOverflow: 'ellipsis',
                        maxWidth: 'min(calc(100vw - 50px), 70vw)',
                    }}
                >
                    <ConnectionItem connection={props.connection} />
                    <Box margin={1} />
                    <ExpireItem
                        ExpireTime={new Date(props.connection.validUntil)}
                        WarrningDaysBefore={3}
                    />
                </TableCell>
                <TableCell>
                    <IconButton
                        title={'Get config'}
                        color="info"
                        onClick={() => props.onConfig?.(props.connection.id)}
                    >
                        <ContentPasteSearch />
                    </IconButton>
                    <IconButton
                        title={'Reset life time'}
                        color="error"
                        onClick={() => props.onReset?.(props.connection.id)}
                    >
                        <Restore />
                    </IconButton>
                    <TextField
                        label="days"
                        value={Number.isInteger(days) ? days : ''}
                        onChange={(e) => {
                            const d = Number.parseInt(e.target.value);
                            return setDays(d >= 0 ? d : 1);
                        }}
                        onFocus={(e) => e.target.select()}
                        type="number"
                        variant="outlined"
                        inputMode="numeric"
                        size="small"
                        style={{ width: '11ch' }}
                    />
                    <IconButton
                        disabled={days === undefined}
                        title={'Add life time'}
                        color="success"
                        onClick={() => {
                            setDays(undefined);
                            props.onExtend?.(props.connection.id, days || 0);
                        }}
                    >
                        <MoreTime />
                    </IconButton>
                </TableCell>
            </TableRow>
        </TableRow>
    );
};

export default ConnectionRow;
