import { LoadingButton } from '@mui/lab';
import { Alert, Box, Button, Divider, PaperProps } from '@mui/material';
import React, { FC, useContext, useState } from 'react';
import { useNavigate, useParams, useSearchParams } from 'react-router-dom';

import { Context } from '../..';
import EasyVpn, {
    ApiError,
    ConnectionTicket,
    PaymentConnectionInfo,
    Server,
} from '../../api';
import CenterBox from '../../components/CenterBox';
import Modal from '../../components/Modal';
import { useRequest, useRequestHandler } from '../../hooks';
import PaymentConnectionForm from '../PaymentConnectionForm';
import ServerSelect from '../ServerSelect';

interface PaymentTikcetModalProps extends PaperProps {
    ticketId?: string;
}

const PaymentTikcetModal: FC<PaymentTikcetModalProps> = (props) => {
    const navigate = useNavigate();
    const handleClose = () => navigate('../.');
    const { Auth } = useContext(Context);

    const ticketId = props.ticketId || useParams().ticketId || '';
    const [days, setDays] = useState<number>(0);

    const [ticket, loading, error] = useRequest<ConnectionTicket, ApiError>(() =>
        EasyVpn.payment.ticket(ticketId, Auth.getToken()).then((v) => v.data),
    );
    const [confirmHandler, confLoading, confError] = useRequestHandler<void, ApiError>(
        () =>
            ticket && days > 0
                ? EasyVpn.payment.confirm(ticket.id, Auth.getToken()).then((v) => v.data)
                : Promise.reject(new Error('Confirm ticket information is not valid!')),
    );
    const [rejectHandler, rejLoading, rejError] = useRequestHandler<void, ApiError>(() =>
        ticket
            ? EasyVpn.payment.confirm(ticket.id, Auth.getToken()).then((v) => v.data)
            : Promise.reject(new Error('Reject ticket information is not valid!')),
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
                    {/*TODO: create connection view component*/}
                    <PaymentConnectionForm paymentInfo={{ ...ticket }} />
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
                            onClick={() => createHandler(() => handleClose())}
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

export default PaymentTikcetModal;
