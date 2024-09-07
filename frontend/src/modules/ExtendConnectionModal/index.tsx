import { LoadingButton } from '@mui/lab';
import { Alert, Box, Button, Divider, PaperProps } from '@mui/material';
import React, { FC, useContext, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';

import { Context } from '../..';
import EasyVpn, { ApiError, PaymentConnectionInfo } from '../../api';
import CenterBox from '../../components/CenterBox';
import Modal from '../../components/Modal';
import { useRequestHandler } from '../../hooks';
import PaymentConnectionForm from '../PaymentConnectionForm';

interface ExtendConnectionModalProps extends PaperProps {
    connectionId?: string;
}

const ExtendConnectionModal: FC<ExtendConnectionModalProps> = (props) => {
    const connectionId = props.connectionId || useParams().connectionId || '';
    const navigate = useNavigate();
    const handleClose = () => navigate('../.');

    const [payInfo, setPayInfo] = useState<PaymentConnectionInfo | null>(null);

    const { Auth } = useContext(Context);
    const [extendHandler, loading, error] = useRequestHandler<void, ApiError>(() =>
        payInfo
            ? EasyVpn.my
                  .extendConnection(
                      { connectionId: connectionId, ...payInfo },
                      Auth.getToken(),
                  )
                  .then((v) => v.data)
            : Promise.reject(new Error('Payment information is not filled!')),
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
                    <Divider textAlign="left">Extend connection {connectionId}</Divider>
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
                            onClick={() => extendHandler(() => handleClose())}
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

export default ExtendConnectionModal;
