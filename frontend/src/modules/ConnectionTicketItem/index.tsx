import { CheckCircleOutline, HighlightOff, Info } from '@mui/icons-material';
import { Box, Chip, Typography } from '@mui/material';
import { format } from 'date-fns';
import React, { FC } from 'react';

import { ConnectionTicket, ConnectionTicketStatus } from '../../api';

interface ConnectionTicketItemProps {
    ticket: ConnectionTicket;
    onGetMoreInfo?: (t: ConnectionTicket) => void;
}

const ConnectionTicketItem: FC<ConnectionTicketItemProps> = (
    props: ConnectionTicketItemProps,
) => {
    const crtt = new Date(props.ticket.creationTime);
    return (
        <Box
            sx={{ maxWidth: 450 }}
            gap={3}
            marginY={1}
            display="flex"
            flexDirection="row"
            alignItems="center"
        >
            <Box width={140}>
                {props.ticket.status == ConnectionTicketStatus.Pending && (
                    <Chip
                        variant="outlined"
                        label="Unprocessed"
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
            </Box>
            <Typography>{format(crtt, 'dd.MM.yyyy HH:mm')}</Typography>
        </Box>
    );
};

export default ConnectionTicketItem;
