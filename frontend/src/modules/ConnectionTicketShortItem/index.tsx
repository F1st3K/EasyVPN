import { CheckCircleOutline, HighlightOff, MoreHoriz } from '@mui/icons-material';
import { Box, Chip, CircularProgress, IconButton, ListItem, ListItemText } from '@mui/material';
import React, { FC } from 'react';

import { ConnectionTicket, ConnectionTicketStatus } from '../../api';

const ConnectionTicketShortItem: FC<ConnectionTicket> = (ticket: ConnectionTicket) => {
    const crtt = new Date(ticket.creationTime);
    return (
        <ListItem sx={{ maxWidth: 450 }}>
            <Box display="flex" flexDirection="row" alignItems="center">
                <ListItemText
                    sx={{ marginRight: 1 }}
                    primary={<>For {ticket.days} day extension</>}
                    secondary={
                        <>
                            from {crtt.toDateString()} at {crtt.getHours().toString().padStart(2, '0')}:
                            {crtt.getMinutes().toString().padStart(2, '0')}
                        </>
                    }
                />
                {ticket.status == ConnectionTicketStatus.Pending && (
                    <Chip variant="outlined" label="Pending..." icon={<CircularProgress disableShrink size={20} />} />
                )}
                {ticket.status == ConnectionTicketStatus.Rejected && (
                    <Chip variant="outlined" label="Rejected" color="error" icon={<HighlightOff />} />
                )}
                {ticket.status == ConnectionTicketStatus.Confirmed && (
                    <Chip variant="outlined" label="Confirmed" color="success" icon={<CheckCircleOutline />} />
                )}
            </Box>
            <IconButton sx={{ marginLeft: 'auto' }} onClick={() => console.log('More clicked')}>
                <MoreHoriz />
            </IconButton>
        </ListItem>
    );
};

export default ConnectionTicketShortItem;
