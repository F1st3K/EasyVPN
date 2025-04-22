import {
    Alert,
    CircularProgress,
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

import { Context } from '../..';
import EasyVpn, { ApiError, Role, User } from '../../api';
import CenterBox from '../../components/CenterBox';
import { useRequest, useRequestHandler } from '../../hooks';
import UserRow from '../../modules/UserRow';

const UsersControlPage: FC = () => {
    const { Auth } = useContext(Context);

    const [data, loading, error] = useRequest<User[], ApiError>(
        () => EasyVpn.users.getAll(Auth.getToken()).then((v) => v.data),
        [location.pathname],
    );
    const [rolesHandler, rolesLoading, rolesError] = useRequestHandler<
        void,
        ApiError,
        { roles: Role[]; id: string }
    >(({ roles, id }) =>
        EasyVpn.users.updateRoles(roles, id, Auth.getToken()).then((v) => v.data),
    );
    const [pwdHandler, pwdLoading, pwdError] = useRequestHandler<
        void,
        ApiError,
        { pwd: string; id: string }
    >(({ pwd, id }) =>
        EasyVpn.users.updatePassword(pwd, id, Auth.getToken()).then((v) => v.data),
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
                        {(rolesLoading || pwdLoading) && <CircularProgress />}
                        {rolesError && (
                            <Alert severity="error" variant="outlined">
                                {rolesError.response?.data.title ?? rolesError.message}
                            </Alert>
                        )}
                        {pwdError && (
                            <Alert severity="error" variant="outlined">
                                {pwdError.response?.data.title ?? pwdError.message}
                            </Alert>
                        )}
                        <Table padding="none">
                            <TableBody>
                                {data?.map((u, key) => (
                                    <UserRow
                                        key={key}
                                        user={u}
                                        currentUserId={Auth.user.id}
                                        onChangeRoles={(id, roles) =>
                                            rolesHandler(
                                                { roles, id },
                                                () => (u.roles = roles),
                                            )
                                        }
                                        onChangePassword={(id, pwd) =>
                                            pwdHandler({ pwd, id })
                                        }
                                    />
                                ))}
                            </TableBody>
                        </Table>
                    </TableContainer>
                </Paper>
            )}
        </CenterBox>
    );
};
export default UsersControlPage;
