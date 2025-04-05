import { LoadingButton } from '@mui/lab';
import { Alert, Box, Button, Divider, PaperProps } from '@mui/material';
import React, { FC, useContext, useState } from 'react';
import { useNavigate, useSearchParams } from 'react-router-dom';

import { Context } from '../..';
import EasyVpn, { ApiError, PaymentConnectionInfo, Server } from '../../api';
import CenterBox from '../../components/CenterBox';
import Modal from '../../components/Modal';
import { useRequestHandler } from '../../hooks';
import PaymentConnectionForm from '../PaymentConnectionForm';
import ServerSelect from '../ServerSelect';

interface CreateConnectionModalProps extends PaperProps {
    serverId?: string;
}

const CreateConnectionModal: FC<CreateConnectionModalProps> = (props) => {
    const [searchParams] = useSearchParams();
    const navigate = useNavigate();
    const handleClose = () => navigate('../.');
    const { Auth } = useContext(Context);

    const serverId = props.serverId || searchParams.get('serverId') || '';
    const [server, setServer] = useState<Server | null>(null);
    const [payInfo, setPayInfo] = useState<PaymentConnectionInfo | null>(null);

    const SetServer = (s: Server | null) => {
        setServer(s);
        navigate(s ? `?serverId=${s.id}` : '');
    };
    const [createHandler, loading, error] = useRequestHandler<void, ApiError>(() =>
        server && payInfo
            ? EasyVpn.my
                  .createConnection({ serverId: server.id, ...payInfo }, Auth.getToken())
                  .then((v) => v.data)
            : Promise.reject(new Error('Server or payment information is not filled!')),
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
                    <Divider textAlign="left">Create new connection</Divider>
                    <ServerSelect serverId={serverId} onChange={SetServer} />
                    <PaymentConnectionForm onChange={setPayInfo} />
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
                            Create connection ticket
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

export default CreateConnectionModal;
