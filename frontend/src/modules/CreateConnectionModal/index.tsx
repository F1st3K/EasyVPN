import { LoadingButton } from '@mui/lab';
import { Alert, AlertTitle, Box, PaperProps } from '@mui/material';
import React, { FC, useContext, useState } from 'react';
import { useNavigate } from 'react-router-dom';

import { Context } from '../..';
import EasyVpn, { ApiError, PaymentConnectionInfo } from '../../api';
import CenterBox from '../../components/CenterBox';
import Modal from '../../components/Modal';
import { useRequestHandler } from '../../hooks';
import PaymentConnectionForm from '../PaymentConnectionForm';

interface CreateConnectionModalProps extends PaperProps {
    connectionId?: string;
}

const CreateConnectionModal: FC<CreateConnectionModalProps> = (props) => {
    const navigate = useNavigate();
    const handleClose = () => navigate('../.');

    const { Auth } = useContext(Context);
    const [createHandler, loading, error] = useRequestHandler<void, ApiError>(
        () => Auth.checkAuth(),
        // EasyVpn.my.createConnection(V, Auth.getToken()).then((v) => v.data),
    );
    const [payInfo, setPayInfo] = useState<PaymentConnectionInfo>();

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
                <CenterBox>
                    <PaymentConnectionForm paymentInfo={payInfo} onChange={setPayInfo} />
                </CenterBox>
            )}
        </Modal>
    );
};

export default CreateConnectionModal;
