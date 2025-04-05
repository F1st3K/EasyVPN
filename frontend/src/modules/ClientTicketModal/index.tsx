import { CheckCircleOutline, HighlightOff, Info } from '@mui/icons-material';
import { Alert, Box, Button, Chip, Divider, PaperProps } from '@mui/material';
import React, { FC, useContext } from 'react';
import { useNavigate, useParams } from 'react-router-dom';

import { Context } from '../..';
import EasyVpn, { ApiError, ConnectionTicket, ConnectionTicketStatus } from '../../api';
import CenterBox from '../../components/CenterBox';
import Modal from '../../components/Modal';
import { useRequest } from '../../hooks';
import ConnectionRequestItem from '../ConnectionRequestItem';
import PaymentConnectionForm from '../PaymentConnectionForm';

interface ClientTicketModalProps extends PaperProps {
    ticketId?: string;
    connecitonId?: string;
}

const ClientTicketModal: FC<ClientTicketModalProps> = (props) => {
    const navigate = useNavigate();
    const handleClose = () => navigate('../.');
    const { Auth } = useContext(Context);

    const ticketId = props.ticketId || useParams().ticketId || '';

    const [ticket, loading, error] = useRequest<ConnectionTicket, ApiError>(
        () => EasyVpn.my.ticket(ticketId, Auth.getToken()).then((v) => v.data),
        [props.about],
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
                            EasyVpn.my
                                .connection(ticket?.connectionId || '', Auth.getToken())
                                .then((v) => v.data)
                        }
                    />
                    <PaymentConnectionForm
                        readonlyDays
                        readonlyDesc
                        readonlyImages
                        paymentInfo={ticket || undefined}
                    />
                    <Divider />
                    <Box
                        display="flex"
                        flexDirection="row-reverse"
                        flexWrap="wrap"
                        justifyContent="space-between"
                        gap={1}
                    >
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

export default ClientTicketModal;
