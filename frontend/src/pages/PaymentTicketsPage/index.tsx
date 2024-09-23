import {
    Alert,
    Divider,
    LinearProgress,
    Paper,
    Table,
    TableBody,
    TableContainer,
    Typography,
} from '@mui/material';
import { observer } from 'mobx-react-lite';
import React, { FC, useContext } from 'react';
import { Outlet, useLocation, useNavigate } from 'react-router-dom';

import { Context } from '../..';
import EasyVpn, { ApiError, ConnectionTicket } from '../../api';
import CenterBox from '../../components/CenterBox';
import { useRequest } from '../../hooks';
import ConnectionTicketRow from '../../modules/ConnectionTicketRow';

const PaymentTicketsPage: FC = () => {
    const store = useContext(Context);
    const navigate = useNavigate();
    const location = useLocation();

    const [data, loading, error] = useRequest<ConnectionTicket[], ApiError>(
        () => EasyVpn.payment.tickets(store.Auth.getToken()).then((v) => v.data),
        [location.pathname],
    );

    if (loading) return <LinearProgress />;

    return (
        <CenterBox>
            {error ? (
                <Alert severity="error" variant="outlined">
                    {error.response?.data.title ?? error.message}
                </Alert>
            ) : (
                <TableContainer>
                    <Typography variant="h5" marginY={3}>
                        Payment tickets:
                    </Typography>
                    <Divider sx={{ borderBottomWidth: '3px' }} />
                    <Table padding="normal">
                        <TableBody>
                            {data?.map((t) => (
                                <ConnectionTicketRow key={t.id} ticket={t} />
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            )}
            <Outlet />
        </CenterBox>
    );
};

export default observer(PaymentTicketsPage);
