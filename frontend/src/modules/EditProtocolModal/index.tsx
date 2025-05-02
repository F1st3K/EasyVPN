import { LoadingButton } from '@mui/lab';
import { Alert, Box, Button, Divider, PaperProps } from '@mui/material';
import React, { FC, useContext, useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';

import { Context } from '../..';
import EasyVpn, { ApiError, Protocol, ProtocolInfo } from '../../api';
import CenterBox from '../../components/CenterBox';
import Modal from '../../components/Modal';
import { useRequest, useRequestHandler } from '../../hooks';
import ProtocolInfoForm from '../ProtocolInfoForm';

interface EditProtocolModalProps extends PaperProps {
    protocolId?: string;
}

const EditProtocolModal: FC<EditProtocolModalProps> = (props) => {
    const navigate = useNavigate();
    const handleClose = () => navigate('../.');
    const { Auth } = useContext(Context);

    const protocolId = props.protocolId || useParams().protocolId || '';
    const [protocol, loading, error] = useRequest<Protocol, ApiError>(() =>
        EasyVpn.protocols.get(protocolId, Auth.getToken()).then((s) => s.data),
    );
    useEffect(() => {
        setProtocolInfo((p) => {
            p.name = protocol?.name || '';
            p.icon = protocol?.icon || '';
            return p;
        });
        return;
    }, [protocol]);

    const [protocolInfo, setProtocolInfo] = useState<ProtocolInfo>({
        name: '',
        icon: '',
    });

    const [editHandler, loadingEdit, errorEdit] = useRequestHandler<void, ApiError>(() =>
        protocolInfo
            ? EasyVpn.protocols
                  .edit(protocolId, protocolInfo, Auth.getToken())
                  .then((v) => v.data)
            : Promise.reject(new Error('Protocol information is not filled!')),
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
                    <Divider textAlign="left">Edit protocol</Divider>
                    <ProtocolInfoForm
                        protocol={protocolInfo}
                        onChange={setProtocolInfo}
                    />
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
                            Edit protocol
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

export default EditProtocolModal;
