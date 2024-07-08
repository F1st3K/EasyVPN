import { ListItem } from '@mui/material';
import Box from '@mui/material/Box';
import React, { FC } from 'react';

import CollapsedListItem from '../../components/CollapsedListItem';

const ClientConnectionsPage: FC = () => {
    return (
        <Box sx={{ minHeight: 352, minWidth: 250 }}>
            <CollapsedListItem item={<>Главный</>}>
                <ListItem>1 el</ListItem>
            </CollapsedListItem>
        </Box>
    );
};

export default ClientConnectionsPage;
