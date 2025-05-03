import { LoadingButton } from '@mui/lab';
import { Alert, Box, Button, Divider, PaperProps } from '@mui/material';
import React, { FC, useContext, useState } from 'react';
import { useNavigate } from 'react-router-dom';

import { Context } from '../..';
import EasyVpn, { ApiError, Server, User } from '../../api';
import CenterBox from '../../components/CenterBox';
import Modal from '../../components/Modal';
import { useRequestHandler } from '../../hooks';
import ClientSelect from '../ClientSelect';
import ServerSelect from '../ServerSelect';

interface CreateShortConnectionModalProps extends PaperProps {
    serverId?: string;
}

const CreateShortConnectionModal: FC<CreateShortConnectionModalProps> = (props) => {
    const navigate = useNavigate();
    const handleClose = () => navigate('../.');
    const { Auth } = useContext(Context);

    const [server, setServer] = useState<Server | null>(null);
    const [client, setClient] = useState<User | null>(null);

    const [createHandler, loading, error] = useRequestHandler<void, ApiError>(() =>
        server && client
            ? EasyVpn.connections
                  .create(server.id, client.id, Auth.getToken())
                  .then((v) => v.data)
            : Promise.reject(new Error('Server or client information is not filled!')),
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
                    <ServerSelect serverId={server?.id} onChange={setServer} />
                    <ClientSelect clientId={client?.id} onChange={setClient} />
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
                            Create connection
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

export default CreateShortConnectionModal;
