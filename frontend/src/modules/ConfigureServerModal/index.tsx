import { LoadingButton } from '@mui/lab';
import { Alert, Box, Button, Divider, PaperProps } from '@mui/material';
import React, { FC, useContext, useEffect, useState } from 'react';
import { useNavigate, useParams, useSearchParams } from 'react-router-dom';

import { Context } from '../..';
import EasyVpn, { ApiError, Server, ServerInfo, VpnVersion } from '../../api';
import CenterBox from '../../components/CenterBox';
import Modal from '../../components/Modal';
import { useRequest, useRequestHandler } from '../../hooks';
import ServerInfoForm from '../ServerForm';

interface ConfigureServerModalProps extends PaperProps {
    protocolId?: string;
    serverId?: string;
}

const ConfigureServerModal: FC<ConfigureServerModalProps> = (props) => {
    const [searchParams] = useSearchParams();
    const navigate = useNavigate();
    const handleClose = () => navigate('../.');
    const { Auth } = useContext(Context);

    const serverId = props.serverId || useParams().serverId || '';
    const [server, loading, error] = useRequest<Server, ApiError>(() =>
        EasyVpn.servers.get(serverId, Auth.getToken()).then((s) => s.data),
    );
    useEffect(() => {
        console.log(server);
        setServerInfo((s) => {
            s.protocolId = server?.protocol.id || '';
            s.version = server?.version || VpnVersion.V1;
            return s;
        });
        return;
    }, [server]);

    const [serverInfo, setServerInfo] = useState<ServerInfo>({
        protocolId: searchParams.get('protocolId') || '',
        version: VpnVersion.V1,
        connection: { auth: '', endpoint: '' },
    });

    const SetServerInfo = (s: ServerInfo) => {
        setServerInfo(s);
        navigate(s ? `?protocolId=${s.protocolId}&version=${s.version}` : '');
    };
    const [editHandler, loadingEdit, errorEdit] = useRequestHandler<void, ApiError>(() =>
        serverInfo
            ? EasyVpn.servers
                  .edit(serverId, serverInfo, Auth.getToken())
                  .then((v) => v.data)
            : Promise.reject(new Error('Server information is not filled!')),
    );

    return (
        <Modal open={true} handleClose={handleClose} loading={loading} {...props}>
            {error ? (
                <Alert
                    onClose={handleClose}
                    severity="error"
                    variant="outlined"
                    sx={{ width: '25ch' }}
                >
                    {error.response?.data.title ?? error.message}
                </Alert>
            ) : (
                <CenterBox
                    display="flex"
                    flexDirection="column"
                    gap={2}
                    paddingX={3}
                    paddingY={1}
                    width="80vw"
                    minWidth="30ch"
                >
                    <Divider textAlign="left">Configure server</Divider>
                    <ServerInfoForm server={serverInfo} onChange={SetServerInfo} />
                    {errorEdit && (
                        <Alert
                            onClose={handleClose}
                            severity="error"
                            variant="outlined"
                            sx={{ width: '25ch' }}
                        >
                            {errorEdit.response?.data.title ?? errorEdit.message}
                        </Alert>
                    )}
                    <Divider />
                    <Box
                        display="flex"
                        flexDirection="row-reverse"
                        flexWrap="wrap"
                        justifyContent="space-between"
                        gap={1}
                    >
                        <LoadingButton
                            variant="contained"
                            color="success"
                            sx={{ textTransform: 'none' }}
                            loading={loadingEdit}
                            onClick={() => editHandler(null, () => handleClose())}
                        >
                            Config server
                        </LoadingButton>
                        <Button
                            variant="outlined"
                            color="inherit"
                            onClick={handleClose}
                            sx={{ textTransform: 'none' }}
                        >
                            Close
                        </Button>
                    </Box>
                </CenterBox>
            )}
        </Modal>
    );
};

export default ConfigureServerModal;
