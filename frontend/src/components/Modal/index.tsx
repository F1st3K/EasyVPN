import { Backdrop, Paper, PaperProps } from '@mui/material';
import React, { FC } from 'react';

interface ModalProps extends PaperProps {
    open?: boolean;
    handleClose?: () => void;
}

const Modal: FC<ModalProps> = (props: ModalProps) => {
    return (
        <Backdrop
            sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1 }}
            open={props.open ?? false}
            onClick={props.handleClose}
        >
            <Paper elevation={24} {...props} />
        </Backdrop>
    );
};

export default Modal;
