import { CheckCircleOutline, HighlightOff, Info } from '@mui/icons-material';
import { LoadingButton } from '@mui/lab';
import { Alert, Box, Button, Chip, Divider, PaperProps } from '@mui/material';
import React, { FC, useContext, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';

import { Context } from '../..';
import EasyVpn, { ApiError, ConnectionTicket, ConnectionTicketStatus } from '../../api';
import CenterBox from '../../components/CenterBox';
import Modal from '../../components/Modal';
import { useRequest, useRequestHandler } from '../../hooks';
import ConnectionRequestItem from '../ConnectionRequestItem';
import PaymentConnectionForm from '../PaymentConnectionForm';

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
                ? EasyVpn.payment
                      .confirm(ticket.id, Auth.getToken(), days)
                      .then((v) => v.data)
                : Promise.reject(new Error('Confirm ticket information is not valid!')),
    );
    const [rejectHandler, rejLoading, rejError] = useRequestHandler<void, ApiError>(() =>
        ticket
            ? EasyVpn.payment.reject(ticket.id, Auth.getToken()).then((v) => v.data)
            : Promise.reject(new Error('Reject ticket information is not valid!')),
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
                    <Divider textAlign="left">Connection ticket</Divider>
                    {ticket?.status == ConnectionTicketStatus.Pending && (
                        <Chip
                            variant="outlined"
                            label="Not done"
                            color="info"
                            icon={<Info />}
                        />
                    )}
                    {ticket?.status == ConnectionTicketStatus.Rejected && (
                        <Chip
                            variant="outlined"
                            label="Rejected"
                            color="error"
                            icon={<HighlightOff />}
                        />
                    )}
                    {ticket?.status == ConnectionTicketStatus.Confirmed && (
                        <Chip
                            variant="outlined"
                            label="Confirmed"
                            color="success"
                            icon={<CheckCircleOutline />}
                        />
                    )}
                    <ConnectionRequestItem
                        connectionPromise={() =>
                            EasyVpn.payment
                                .connection(ticket?.connectionId || '', Auth.getToken())
                                .then((v) => v.data)
                        }
                    />
                    <PaymentConnectionForm
                        readonlyDays={ticket?.status != ConnectionTicketStatus.Pending}
                        readonlyDesc
                        readonlyImages
                        paymentInfo={ticket || undefined}
                        onChange={(p) => setDays(p.days)}
                    />
                    {rejError && (
                        <Alert severity="error" variant="outlined">
                            {rejError.response?.data.title ?? rejError.message}
                        </Alert>
                    )}
                    {confError && (
                        <Alert severity="error" variant="outlined">
                            {confError.response?.data.title ?? confError.message}
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
                        {ticket?.status == ConnectionTicketStatus.Pending && (
                            <Box flexWrap="nowrap">
                                <LoadingButton
                                    variant="contained"
                                    color="error"
                                    sx={{ textTransform: 'none' }}
                                    loading={rejLoading}
                                    disabled={confLoading}
                                    loadingPosition="start"
                                    startIcon={<HighlightOff />}
                                    onClick={() =>
                                        rejectHandler(null, () => handleClose())
                                    }
                                >
                                    Reject
                                </LoadingButton>
                                <LoadingButton
                                    variant="contained"
                                    color="success"
                                    sx={{ marginLeft: '1ch', textTransform: 'none' }}
                                    loading={confLoading}
                                    disabled={rejLoading}
                                    loadingPosition="start"
                                    startIcon={<CheckCircleOutline />}
                                    onClick={() =>
                                        confirmHandler(null, () => handleClose())
                                    }
                                >
                                    Confirm
                                </LoadingButton>
                            </Box>
                        )}
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
