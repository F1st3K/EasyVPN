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
import React, { useContext } from 'react';
import { FC } from 'react';
import { Outlet, useNavigate } from 'react-router-dom';

import { Context } from '../..';
import EasyVpn, { ApiError, Server } from '../../api';
import CenterBox from '../../components/CenterBox';
import CreateButton from '../../components/CreateButton';
import { useRequest } from '../../hooks';

const ServersControlPage: FC = () => {
    const { Auth } = useContext(Context);
    const navigate = useNavigate();

    const [data, loading, error] = useRequest<Server[], ApiError>(
        () => EasyVpn.servers.getAll(Auth.getToken()).then((v) => v.data),
        [location.pathname],
    );

    if (loading) return <LinearProgress />;

    return (
        <CenterBox margin={2}>
            {error ? (
                <Alert severity="error" variant="outlined">
                    {error.response?.data.title ?? error.message}
                </Alert>
            ) : (
                <Paper sx={{ borderRadius: 2, paddingBottom: '10px' }}>
                    <TableContainer>
                        <Typography variant="h5" padding={3}>
                            Servers:
                        </Typography>
                        <CreateButton onClick={() => navigate('./new')} />
                        <Divider sx={{ borderBottomWidth: '3px' }} />
                        <Table padding="none">
                            <TableBody>
                                {data?.map((s, key) => <p key={key}>{s.id}</p>)}
                            </TableBody>
                        </Table>
                    </TableContainer>
                </Paper>
            )}
            <Outlet />
        </CenterBox>
    );
};
export default ServersControlPage;
