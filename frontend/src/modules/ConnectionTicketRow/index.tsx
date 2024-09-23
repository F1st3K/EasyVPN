import { CheckCircleOutline, HighlightOff, Info, OpenInNew } from '@mui/icons-material';
import { Box, Chip, IconButton, TableCell, TableRow, Typography } from '@mui/material';
import { format } from 'date-fns';
import React, { FC } from 'react';

import { ConnectionTicket, ConnectionTicketStatus } from '../../api';
import PaymentTicketsPage from '../../pages/PaymentTicketsPage';

interface ConnectionTicketRowProps {
    ticket: ConnectionTicket;
    onGetMoreInfo?: (t: ConnectionTicket) => void;
}

const ConnectionTicketRow: FC<ConnectionTicketRowProps> = (
    props: ConnectionTicketRowProps,
) => {
    const crtt = new Date(props.ticket.creationTime);
    return (
        <TableRow hover>
            <TableCell>
                {props.ticket.status == ConnectionTicketStatus.Pending && (
                    <Chip
                        variant="outlined"
                        label="Not done"
                        color="info"
                        icon={<Info />}
                    />
                )}
                {props.ticket.status == ConnectionTicketStatus.Rejected && (
                    <Chip
                        variant="outlined"
                        label="Rejected"
                        color="error"
                        icon={<HighlightOff />}
                    />
                )}
                {props.ticket.status == ConnectionTicketStatus.Confirmed && (
                    <Chip
                        variant="outlined"
                        label="Confirmed"
                        color="success"
                        icon={<CheckCircleOutline />}
                    />
                )}
            </TableCell>
            <TableCell>
                <Chip size="small" label={format(crtt, 'HH:mm dd.MM.yyyy')} />
            </TableCell>
            <TableCell>
                {props.ticket.client.firstName} {props.ticket.client.lastName}
            </TableCell>
            <TableCell
                sx={{
                    whiteSpace: 'nowrap',
                    overflow: 'hidden',
                    textOverflow: 'ellipsis',
                    maxWidth: 'min(calc(100vw - 600px), 15vw)',
                }}
            >
                <Typography variant="caption" color="Gray">
                    Some body test text for example ffffffffffffffffffffffffffffffffffff.
                    sfasf sjadflkasdjflkj sadjf sdlkjf lkjasd lkjsadklf jsjd slfjsdljf
                    jsdf kjsdkfj sjdf skdj jhhkkh
                </Typography>
            </TableCell>
            <TableCell>
                <IconButton
                    onClick={() =>
                        props.onGetMoreInfo && props.onGetMoreInfo(props.ticket)
                    }
                >
                    <OpenInNew />
                </IconButton>
            </TableCell>
        </TableRow>
    );
};

export default ConnectionTicketRow;
