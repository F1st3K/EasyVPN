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
import { useNavigate } from 'react-router-dom';

import { Context } from '../..';
import EasyVpn, { ApiError, User } from '../../api';
import Page from '../../api/requests/Page';
import CenterBox from '../../components/CenterBox';
import { useRequest, useRequestHandler } from '../../hooks';
import UserRow from '../../modules/UserRow';

const UsersControlPage: FC = () => {
    const navigate = useNavigate();
    const { Auth } = useContext(Context);
    const { Pages } = useContext(Context);

    const [data, loading, error] = useRequest<User[], ApiError>(
        () => EasyVpn.users.getAll(Auth.getToken()).then((v) => v.data),
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
                            Users:
                        </Typography>
                        <Divider sx={{ borderBottomWidth: '3px' }} />
                        <Table padding="none">
                            <TableBody>
                                {data?.map((u, key) => <UserRow key={key} user={u} />)}
                            </TableBody>
                        </Table>
                    </TableContainer>
                </Paper>
            )}
        </CenterBox>
    );
};
export default UsersControlPage;
