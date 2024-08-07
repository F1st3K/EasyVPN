import { Alert, CircularProgress, LinearProgress } from '@mui/material';
import { observer } from 'mobx-react-lite';
import React, { FC, useContext } from 'react';
import { Outlet } from 'react-router-dom';

import { Context } from '../..';
import EasyVpn, { ApiError, Connection, ConnectionTicket } from '../../api';
import CenterBox from '../../components/CenterBox';
import CollapsedListItem from '../../components/CollapsedListItem';
import { useRequest } from '../../hooks';
import ConnectionShortItem from '../../modules/ConnectionShortItem';
import ConnectionTicketShortItem from '../../modules/ConnectionTicketShortItem';

const ClientConnectionsPage: FC = () => {
    const store = useContext(Context);
    const [data, loading, error] = useRequest<Connection[], ApiError>(() =>
        EasyVpn.my.connections(store.Auth.getToken()).then((v) => v.data),
    );

    const [tdata, tloading, terror] = useRequest<ConnectionTicket[], ApiError>(() =>
        EasyVpn.my.tickets(store.Auth.getToken()).then((v) => v.data),
    );

    if (loading) return <LinearProgress />;

    return (
        <CenterBox>
            {error ? (
                <Alert severity="error" variant="outlined">
                    {error.response?.data.title ?? error.message}
                </Alert>
            ) : (
                data?.map((c) => (
                    <CollapsedListItem
                        key={c.id}
                        item={<ConnectionShortItem connection={c} />}
                        listTooltip="Tickets"
                    >
                        {tloading && <CircularProgress sx={{ marginLeft: 4 }} />}
                        {terror && (
                            <Alert
                                severity="error"
                                sx={{ width: '25ch' }}
                                variant="outlined"
                            >
                                {terror.response?.data.title ?? terror.message}
                            </Alert>
                        )}
                        {tloading == false &&
                            terror == null &&
                            tdata
                                ?.filter((t) => t.connectionId == c.id)
                                .map((t) => (
                                    <ConnectionTicketShortItem key={t.id} ticket={t} />
                                ))}
                    </CollapsedListItem>
                ))
            )}
            <Outlet />
        </CenterBox>
    );
};

export default observer(ClientConnectionsPage);
