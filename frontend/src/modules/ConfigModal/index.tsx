import { Download } from '@mui/icons-material';
import { Alert, Button, Divider, IconButton, useTheme } from '@mui/material';
import jsFileDownload from 'js-file-download';
import React, { FC, useContext } from 'react';
import QRCode from 'react-qr-code';

import { Context } from '../..';
import EasyVpn, { ApiError } from '../../api';
import CenterBox from '../../components/CenterBox';
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
                .then((v) => v.data.config),
        [props.open],
    );

    const theme = useTheme();
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
                <CenterBox paddingX={3} paddingY={1}>
                    <Divider>Scan QR code</Divider>
                    <CenterBox marginY={1}>
                        <QRCode
                            fgColor={
                                theme.palette.mode === 'light'
                                    ? theme.palette.text.primary
                                    : 'rgba(0, 0, 0, 0)'
                            }
                            bgColor={
                                theme.palette.mode === 'dark'
                                    ? theme.palette.text.primary
                                    : 'rgba(0, 0, 0, 0)'
                            }
                            value={config || ''}
                            size={400}
                            style={{
                                height: 'auto',
                                maxWidth: '100%',
                                width: '100%',
                                border: '5px solid',
                                borderColor:
                                    theme.palette.mode === 'dark'
                                        ? theme.palette.text.primary
                                        : 'rgba(0, 0, 0, 0)',
                                borderRadius: '15px',
                            }}
                            level="M"
                        />
                    </CenterBox>
                    <Divider>OR</Divider>
                    <CenterBox marginY={1}>
                        <IconButton
                            onClick={() =>
                                jsFileDownload(config || '', `${connectionId}.conf`)
                            }
                        >
                            <Download />
                        </IconButton>
                        <CopyButton text={config || ''} />
                    </CenterBox>
                    <Divider></Divider>
                    <CenterBox marginTop={1}>
                        <Button variant="contained" onClick={props.handleClose}>
                            Close
                        </Button>
                    </CenterBox>
                </CenterBox>
            )}
        </Modal>
    );
};

export default ConfigModal;
