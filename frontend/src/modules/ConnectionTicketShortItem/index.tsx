import { CheckCircleOutline, HighlightOff, OpenInNew } from '@mui/icons-material';
import {
    Box,
    Chip,
    CircularProgress,
    IconButton,
    ListItem,
    ListItemText,
} from '@mui/material';
import React, { FC } from 'react';

import { ConnectionTicket, ConnectionTicketStatus } from '../../api';

interface ConnectionTicketShortItemProps {
    ticket: ConnectionTicket;
    onGetMoreInfo?: (t: ConnectionTicket) => void;
}

const ConnectionTicketShortItem: FC<ConnectionTicketShortItemProps> = (
    props: ConnectionTicketShortItemProps,
) => {
    const crtt = new Date(props.ticket.creationTime);
    return (
        <ListItem sx={{ maxWidth: 450 }}>
            <Box display="flex" flexDirection="row" alignItems="center">
                <ListItemText
                    sx={{ marginRight: 1 }}
                    primary={<>For {props.ticket.days} day extension</>}
                    secondary={
                        <>
                            from {crtt.toDateString()} at{' '}
                            {crtt.getHours().toString().padStart(2, '0')}:
                            {crtt.getMinutes().toString().padStart(2, '0')}
                        </>
                    }
                />
                {props.ticket.status == ConnectionTicketStatus.Pending && (
                    <Chip
                        variant="outlined"
                        label="Pending..."
                        icon={<CircularProgress disableShrink size={20} />}
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
            <IconButton
                sx={{ marginLeft: 'auto' }}
                onClick={() => props.onGetMoreInfo && props.onGetMoreInfo(props.ticket)}
            >
                <OpenInNew />
            </IconButton>
        </ListItem>
    );
};

export default ConnectionTicketShortItem;
