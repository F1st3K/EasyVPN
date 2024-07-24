import { Alert, Button } from '@mui/material';
import jsFileDownload from 'js-file-download';
import React, { FC, useContext } from 'react';
import QRCode from 'react-qr-code';

import { Context } from '../..';
import EasyVpn, { ApiError } from '../../api';
import CopyButton from '../../components/CopyButton';
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
                <>
                    <QRCode value={config || ''} />
                    <Button
                        onClick={() =>
                            jsFileDownload(config || '', `${connectionId}.conf`)
                        }
                    >
                        Download file
                    </Button>
                    <CopyButton text={config || ''} />
                </>
            )}
        </Modal>
    );
};

export default ConfigModal;
