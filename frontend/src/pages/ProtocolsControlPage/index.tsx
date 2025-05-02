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
import EasyVpn, { ApiError, Protocol } from '../../api';
import CenterBox from '../../components/CenterBox';
import CreateButton from '../../components/CreateButton';
import Modal from '../../components/Modal';
import { useRequest, useRequestHandler } from '../../hooks';
import ProtocolRow from '../../modules/ProtocolRow';

const ProtocolsControlPage: FC = () => {
    const { Auth } = useContext(Context);
    const navigate = useNavigate();

    const [data, loading, error] = useRequest<Protocol[], ApiError>(
        () => EasyVpn.protocols.getAll(Auth.getToken()).then((v) => v.data),
        [location.pathname],
    );

    const [removeProtocolId, setRemoveProtocolId] = useState<string>();
    const [removeHandler, loadingRm, errorRm] = useRequestHandler<void, ApiError>(() =>
        EasyVpn.protocols
            .delete(removeProtocolId || '', Auth.getToken())
            .then((r) => r.data),
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
                            Protocols:
                        </Typography>
                        <CreateButton onClick={() => navigate('./new')} />
                        <Divider sx={{ borderBottomWidth: '3px' }} />
                        <Table padding="none">
                            <TableBody>
                                {data?.map((p, key) => (
                                    <ProtocolRow
                                        key={key}
                                        protocol={p}
                                        onEdit={(id) => navigate(`./${id}`)}
                                        onRemove={(id) => setRemoveProtocolId(id)}
                                    />
                                ))}
                            </TableBody>
                        </Table>
                    </TableContainer>
                </Paper>
            )}
            <Modal
                open={removeProtocolId !== undefined}
                handleClose={() => setRemoveProtocolId(undefined)}
            >
                {errorRm && (
                    <Alert severity="error" variant="outlined">
                        {errorRm.response?.data.title ?? errorRm.message}
                    </Alert>
                )}
                <Alert
                    onClose={() => setRemoveProtocolId(undefined)}
                    severity="warning"
                    variant="outlined"
                >
                    <AlertTitle>Delete protocol?</AlertTitle>
                    <>Do you really want delete protocol {removeProtocolId}?</>
                    <Box marginTop={1} display="flex" flexDirection="row-reverse">
                        <LoadingButton
                            color="warning"
                            sx={{ textTransform: 'none' }}
                            variant="contained"
                            loading={loadingRm}
                            onClick={() =>
                                removeHandler(null, async () => {
                                    data?.splice(
                                        data?.findIndex((s) => s.id === removeProtocolId),
                                        1,
                                    );
                                    setRemoveProtocolId(undefined);
                                })
                            }
                        >
                            Yes, remove protocol
                        </LoadingButton>
                    </Box>
                </Alert>
            </Modal>
            <Outlet />
        </CenterBox>
    );
};
export default ProtocolsControlPage;
