import { Alert } from '@mui/material';
import React, { FC, useContext } from 'react';

import { Context } from '../..';
import EasyVpn, { ApiError } from '../../api';
import Modal from '../../components/Modal';
import { useRequest } from '../../hooks';

interface ConfigModalProps {
    connectionId: string;
    open?: boolean;
    handleClose?: () => void;
}

const ConfigModal: FC<ConfigModalProps> = ({ connectionId, ...props }) => {
    if (props.open == false) return null;

    const { Auth } = useContext(Context);
    const [config, loading, error] = useRequest<string, ApiError>(
        () =>
            EasyVpn.my
                .configConnection(connectionId, Auth.getToken())
                .then((v) => v.data.toString()),
        [props.open],
    );
    return (
        <Modal loading={loading} {...props}>
            {error ? (
                <Alert
                    onClose={props.handleClose}
                    severity="error"
                    variant="outlined"
                    sx={{ width: '25ch' }}
                >
                    {error.response?.data.title ?? error.message}
                </Alert>
            ) : (
                config
            )}
        </Modal>
    );
};

export default ConfigModal;
