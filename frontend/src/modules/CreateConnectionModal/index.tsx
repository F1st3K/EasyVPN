import { LoadingButton } from '@mui/lab';
import { Alert, AlertTitle, Box, PaperProps } from '@mui/material';
import React, { FC, useContext } from 'react';
import { useNavigate } from 'react-router-dom';

import { Context } from '../..';
import EasyVpn, { ApiError } from '../../api';
import Modal from '../../components/Modal';
import { useRequestHandler } from '../../hooks';

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
                <Alert onClose={handleClose} severity="warning" variant="outlined">
                    <AlertTitle>Delete connection?</AlertTitle>
                    <Box marginTop={1}>
                        <LoadingButton
                            color="warning"
                            sx={{ textTransform: 'none' }}
                            variant="contained"
                            loading={loading}
                            onClick={() => createHandler(() => handleClose())}
                        >
                            Yes, remove connection
                        </LoadingButton>
                    </Box>
                </Alert>
            )}
        </Modal>
    );
};

export default CreateConnectionModal;
