import { Alert, CircularProgress, LinearProgress } from '@mui/material';
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

    return (
        <Box width="100%" alignContent={'center'}>
            {loading && <LinearProgress />}
            {error && (
                <Alert severity="error" sx={{ width: '25ch' }}>
                    {error.response?.data.title ?? error.message}
                </Alert>
            )}
            {loading == false &&
                error == null &&
                data?.map((c) => (
                    <CollapsedListItem key={c.id} item={<ConnectionShortItem connection={c} />} listTooltip="Tickets">
                        {tloading && <CircularProgress sx={{ marginLeft: '30%' }} />}
                        {terror && (
                            <Alert severity="error" sx={{ width: '25ch' }}>
                                {terror.response?.data.title ?? terror.message}
                            </Alert>
                        )}
                        {tloading == false &&
                            terror == null &&
                            tdata
                                ?.filter((t) => t.connectionId == c.id)
                                .map((t) => <ConnectionTicketShortItem key={t.id} ticket={t} />)}
                    </CollapsedListItem>
                ))}
        </Box>
    );
};

export default observer(ClientConnectionsPage);
