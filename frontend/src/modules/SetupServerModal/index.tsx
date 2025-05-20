import { LoadingButton } from '@mui/lab';
import { Alert, Box, Button, Divider, PaperProps } from '@mui/material';
import React, { FC, useContext, useState } from 'react';
import { useNavigate, useSearchParams } from 'react-router-dom';

import { Context } from '../..';
import EasyVpn, { ApiError, ServerInfo, VpnVersion } from '../../api';
import CenterBox from '../../components/CenterBox';
import Modal from '../../components/Modal';
import { useRequestHandler } from '../../hooks';
import ServerInfoForm from '../ServerForm';

interface SetupServerModalProps extends PaperProps {
    protocolId?: string;
}

const SetupServerModal: FC<SetupServerModalProps> = (props) => {
    const [searchParams] = useSearchParams();
    const navigate = useNavigate();
    const handleClose = () => navigate('../.');
    const { Auth } = useContext(Context);

    const protocolId = props.protocolId || searchParams.get('protocolId') || '';
    const [serverInfo, setServerInfo] = useState<ServerInfo>({
        protocolId,
        version: VpnVersion.V1,
        connection: { auth: '', endpoint: '' },
    });

    const SetServerInfo = (s: ServerInfo) => {
        setServerInfo(s);
        navigate(s ? `?protocolId=${s.protocolId}` : '');
    };
    const [createHandler, loading, error] = useRequestHandler<void, ApiError>(() =>
        serverInfo
            ? EasyVpn.servers.create(serverInfo, Auth.getToken()).then((v) => v.data)
            : Promise.reject(new Error('Server information is not filled!')),
    );

    return (
        <Modal open={true} handleClose={handleClose} {...props}>
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
                    <Divider textAlign="left">Setup new server</Divider>
                    <ServerInfoForm server={serverInfo} onChange={SetServerInfo} />
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
                            loading={loading}
                            onClick={() => createHandler(null, () => handleClose())}
                        >
                            Setup server
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

export default SetupServerModal;
