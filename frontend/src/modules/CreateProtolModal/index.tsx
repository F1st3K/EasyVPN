import { LoadingButton } from '@mui/lab';
import { Alert, Box, Button, Divider, PaperProps } from '@mui/material';
import React, { FC, useContext, useState } from 'react';
import { useNavigate } from 'react-router-dom';

import { Context } from '../..';
import EasyVpn, { ApiError, ProtocolInfo } from '../../api';
import CenterBox from '../../components/CenterBox';
import Modal from '../../components/Modal';
import { useRequestHandler } from '../../hooks';
import ProtocolInfoForm from '../ProtocolInfoForm';

interface CreateProtocolModalProps extends PaperProps {
    protocolId?: string;
}

const CreateProtocolModal: FC<CreateProtocolModalProps> = (props) => {
    const navigate = useNavigate();
    const handleClose = () => navigate('../.');
    const { Auth } = useContext(Context);

    const [protocolInfo, setProtocolInfo] = useState<ProtocolInfo>({
        name: '',
        icon: '',
    });

    const [createHandler, loading, error] = useRequestHandler<void, ApiError>(() =>
        protocolInfo
            ? EasyVpn.protocols.create(protocolInfo, Auth.getToken()).then((v) => v.data)
            : Promise.reject(new Error('Protocol information is not filled!')),
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
                    <Divider textAlign="left">Create new protocol</Divider>
                    <ProtocolInfoForm
                        protocol={protocolInfo}
                        onChange={setProtocolInfo}
                    />
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
                            Create protocol
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

export default CreateProtocolModal;
