import { LoadingButton } from '@mui/lab';
import { Alert, AlertTitle, Box, PaperProps } from '@mui/material';
import React, { FC, useContext } from 'react';
import { useNavigate, useParams } from 'react-router-dom';

import { Context } from '../..';
import EasyVpn, { ApiError } from '../../api';
import Modal from '../../components/Modal';
import { useRequestHandler } from '../../hooks';
import ConnectionRequestItem from '../ConnectionRequestItem';

interface DeleteConnectionModalProps extends PaperProps {
    connectionId?: string;
}

const DeleteConnectionModal: FC<DeleteConnectionModalProps> = (props) => {
    const connectionId = props.connectionId || useParams().connectionId || '';
    const navigate = useNavigate();
    const handleClose = () => navigate('../.');

    const { Auth } = useContext(Context);
    const [deleteHandler, loading, error] = useRequestHandler<void, ApiError>(() =>
        EasyVpn.my.deleteConnection(connectionId, Auth.getToken()).then((v) => v.data),
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
                    <>Do you really want delete connection?</>
                    <ConnectionRequestItem
                        connectionPromise={() =>
                            EasyVpn.my
                                .connection(connectionId, Auth.getToken())
                                .then((c) => c.data)
                        }
                    />
                    <Box marginTop={1} display="flex" flexDirection="row-reverse">
                        <LoadingButton
                            color="warning"
                            sx={{ textTransform: 'none' }}
                            variant="contained"
                            loading={loading}
                            onClick={() => deleteHandler(null, () => handleClose())}
                        >
                            Yes, remove connection
                        </LoadingButton>
                    </Box>
                </Alert>
            )}
        </Modal>
    );
};

export default DeleteConnectionModal;
