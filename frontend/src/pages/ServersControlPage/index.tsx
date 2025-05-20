import { LoadingButton } from '@mui/lab';
import {
    Alert,
    AlertTitle,
    Box,
    Divider,
    LinearProgress,
    Paper,
    Table,
    TableBody,
    TableContainer,
    Typography,
} from '@mui/material';
import React, { useContext, useState } from 'react';
import { FC } from 'react';
import { Outlet, useNavigate } from 'react-router-dom';

import { Context } from '../..';
import EasyVpn, { ApiError, Server } from '../../api';
import CenterBox from '../../components/CenterBox';
import CreateButton from '../../components/CreateButton';
import Modal from '../../components/Modal';
import { useRequest, useRequestHandler } from '../../hooks';
import ServerRow from '../../modules/ServerRow';

const ServersControlPage: FC = () => {
    const { Auth } = useContext(Context);
    const navigate = useNavigate();

    const [data, loading, error] = useRequest<Server[], ApiError>(
        () => EasyVpn.servers.getAll(Auth.getToken()).then((v) => v.data),
        [location.pathname],
    );

    const [removeServerId, setRemoveServerId] = useState<string>();
    const [removeHandler, loadingRm, errorRm] = useRequestHandler<void, ApiError>(() =>
        EasyVpn.servers.delete(removeServerId || '', Auth.getToken()).then((r) => r.data),
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
                                {data?.map((s, key) => (
                                    <ServerRow
                                        key={key}
                                        server={s}
                                        onEdit={(id) => navigate(`./${id}`)}
                                        onRemove={(id) => setRemoveServerId(id)}
                                    />
                                ))}
                            </TableBody>
                        </Table>
                    </TableContainer>
                </Paper>
            )}
            <Modal
                open={removeServerId !== undefined}
                handleClose={() => setRemoveServerId(undefined)}
            >
                {errorRm && (
                    <Alert severity="error" variant="outlined">
                        {errorRm.response?.data.title ?? errorRm.message}
                    </Alert>
                )}
                <Alert
                    onClose={() => setRemoveServerId(undefined)}
                    severity="warning"
                    variant="outlined"
                >
                    <AlertTitle>Delete page?</AlertTitle>
                    <>Do you really want delete server {removeServerId}?</>
                    <Box marginTop={1} display="flex" flexDirection="row-reverse">
                        <LoadingButton
                            color="warning"
                            sx={{ textTransform: 'none' }}
                            variant="contained"
                            loading={loadingRm}
                            onClick={() =>
                                removeHandler(null, async () => {
                                    data?.splice(
                                        data?.findIndex((s) => s.id === removeServerId),
                                        1,
                                    );
                                    setRemoveServerId(undefined);
                                })
                            }
                        >
                            Yes, remove page
                        </LoadingButton>
                    </Box>
                </Alert>
            </Modal>
            <Outlet />
        </CenterBox>
    );
};
export default ServersControlPage;
