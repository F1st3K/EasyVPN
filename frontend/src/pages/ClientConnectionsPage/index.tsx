import Box from '@mui/material/Box';
import { observer } from 'mobx-react-lite';
import React, { FC, useContext } from 'react';

import { Context } from '../..';
import EasyVpn, { ApiError, Connection, ConnectionTicket } from '../../api';
import CollapsedListItem from '../../components/CollapsedListItem';
import { useRequest } from '../../hooks';
import ConnectionShortItem from '../../modules/ConnectionShortItem';
import ConnectionTicketShortItem from '../../modules/ConnectionTicketShortItem';

const ClientConnectionsPage: FC = () => {
    const store = useContext(Context);
    const [data, loading, error] = useRequest<Connection[], ApiError>(() =>
        EasyVpn.my.connections(store.Auth.getToken() ?? '').then((v) => v.data),
    );

    const [tdata, tloading, terror] = useRequest<ConnectionTicket[], ApiError>(() =>
        EasyVpn.my.tickets(store.Auth.getToken() ?? '').then((v) => v.data),
    );

    if (loading) return <>Loading...</>;

    if (error) return <>Is Error</>;

    if (tloading) return <>tLoading...</>;

    if (terror) return <>tIs Error</>;

    return (
        <Box>
            {data?.map((c) => (
                <CollapsedListItem key={c.id} item={<ConnectionShortItem {...c} />} listTooltip="Tickets">
                    {tdata
                        ?.filter((t) => t.connectionId == c.id)
                        .map((t) => <ConnectionTicketShortItem key={t.id} {...t} />)}
                </CollapsedListItem>
            ))}
        </Box>
    );
};

export default observer(ClientConnectionsPage);
