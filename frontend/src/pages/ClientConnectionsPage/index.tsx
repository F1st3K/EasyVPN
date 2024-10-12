import {
    Alert,
    CircularProgress,
    LinearProgress,
    Paper,
    Typography,
} from '@mui/material';
import { observer } from 'mobx-react-lite';
import React, { FC, useContext } from 'react';
import { Outlet, useLocation, useNavigate } from 'react-router-dom';

import { Context } from '../..';
import EasyVpn, { ApiError, Connection, ConnectionTicket } from '../../api';
import CenterBox from '../../components/CenterBox';
import CollapsedListItem from '../../components/CollapsedListItem';
import CreateButton from '../../components/CreateButton';
import { useRequest } from '../../hooks';
import ConnectionShortItem from '../../modules/ConnectionShortItem';
import ConnectionTicketShortItem from '../../modules/ConnectionTicketShortItem';

const ClientConnectionsPage: FC = () => {
    const store = useContext(Context);
    const navigate = useNavigate();
    const location = useLocation();

    const [data, loading, error] = useRequest<Connection[], ApiError>(
        () => EasyVpn.my.connections(store.Auth.getToken()).then((v) => v.data),
        [location.pathname],
    );

    const [tdata, tloading, terror] = useRequest<ConnectionTicket[], ApiError>(
        () => EasyVpn.my.tickets(store.Auth.getToken()).then((v) => v.data),
        [location.pathname],
    );

    if (loading) return <LinearProgress />;

    return (
        <CenterBox margin={2}>
            <Paper sx={{ borderRadius: 2, padding: '10px' }}>
                <CreateButton
                    onClick={() => navigate('./new')}
                    description={
                        <CenterBox>
                            <Typography>
                                Want to create a fast and reliable connection?
                            </Typography>
                            <Typography fontWeight="bold">
                                Just click and get it!
                            </Typography>
                        </CenterBox>
                    }
                />
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
                                        <ConnectionTicketShortItem
                                            key={t.id}
                                            ticket={t}
                                            onGetMoreInfo={(t) =>
                                                navigate(`ticket/${t.id}`)
                                            }
                                        />
                                    ))}
                        </CollapsedListItem>
                    ))
                )}
            </Paper>
            <Outlet />
        </CenterBox>
    );
};

export default observer(ClientConnectionsPage);
