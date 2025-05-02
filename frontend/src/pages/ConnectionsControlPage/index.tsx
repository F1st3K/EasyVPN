import { LoadingButton } from '@mui/lab';
import {
    Alert,
    AlertTitle,
    Box,
    CircularProgress,
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
import EasyVpn, { ApiError, Connection } from '../../api';
import CenterBox from '../../components/CenterBox';
import CreateButton from '../../components/CreateButton';
import Modal from '../../components/Modal';
import { useRequest, useRequestHandler } from '../../hooks';
import ConnectionRow from '../../modules/ConnectionRow';

const ConnectionsControlPage: FC = () => {
    const { Auth } = useContext(Context);
    const navigate = useNavigate();

    const [data, loading, error] = useRequest<Connection[], ApiError>(
        () => EasyVpn.connections.getAll(Auth.getToken()).then((v) => v.data),
        [location.pathname],
    );

    const [resetConnectionId, setResetConnectionId] = useState<string>();
    const [removeHandler, loadingRm, errorRm] = useRequestHandler<void, ApiError>(() =>
        EasyVpn.connections
            .reset(resetConnectionId || '', Auth.getToken())
            .then((r) => r.data),
    );

    const [extendHandler, loadingEx, errorEx] = useRequestHandler<
        void,
        ApiError,
        { id: string; days: number }
    >((p) =>
        EasyVpn.connections.extend(p.id, p.days, Auth.getToken()).then((r) => r.data),
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
                            Connections:
                        </Typography>
                        <CreateButton onClick={() => navigate('./new')} />
                        <Divider sx={{ borderBottomWidth: '3px' }} />
                        {loadingEx && <CircularProgress />}
                        {errorEx && (
                            <Alert severity="error" variant="outlined">
                                {errorEx.response?.data.title ?? errorEx.message}
                            </Alert>
                        )}
                        <Table padding="none">
                            <TableBody>
                                {data?.map((c, key) => (
                                    <ConnectionRow
                                        key={key}
                                        connection={c}
                                        onExtend={(id, days) =>
                                            extendHandler({ id, days })
                                        }
                                        onReset={(id) => setResetConnectionId(id)}
                                    />
                                ))}
                            </TableBody>
                        </Table>
                    </TableContainer>
                </Paper>
            )}
            <Modal
                open={resetConnectionId !== undefined}
                handleClose={() => setResetConnectionId(undefined)}
            >
                {errorRm && (
                    <Alert severity="error" variant="outlined">
                        {errorRm.response?.data.title ?? errorRm.message}
                    </Alert>
                )}
                <Alert
                    onClose={() => setResetConnectionId(undefined)}
                    severity="warning"
                    variant="outlined"
                >
                    <AlertTitle>Reset life time connection?</AlertTitle>
                    <>Do you really want reset connection {resetConnectionId}?</>
                    <Box marginTop={1} display="flex" flexDirection="row-reverse">
                        <LoadingButton
                            color="warning"
                            sx={{ textTransform: 'none' }}
                            variant="contained"
                            loading={loadingRm}
                            onClick={() =>
                                removeHandler(null, async () => {
                                    data?.splice(
                                        data?.findIndex(
                                            (s) => s.id === resetConnectionId,
                                        ),
                                        1,
                                    );
                                    setResetConnectionId(undefined);
                                })
                            }
                        >
                            Yes, reset connection
                        </LoadingButton>
                    </Box>
                </Alert>
            </Modal>
            <Outlet />
        </CenterBox>
    );
};
export default ConnectionsControlPage;
