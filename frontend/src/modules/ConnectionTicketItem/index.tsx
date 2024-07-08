import { ListItem, ListItemText } from '@mui/material';
import React, { FC } from 'react';

import { ConnectionTicket } from '../../api';

const ConnectionTicketItem: FC<ConnectionTicket> = (ticket: ConnectionTicket) => {
    return (
        <ListItem>
            <ListItemText
                primary={<>Activate for {ticket.days} days</>}
                secondary={
                    <>
                        from {new Date(ticket.creationTime).toDateString()} at{' '}
                        {new Date(ticket.creationTime).toTimeString()}
                    </>
                }
            />
        </ListItem>
    );
};

export default ConnectionTicketItem;
