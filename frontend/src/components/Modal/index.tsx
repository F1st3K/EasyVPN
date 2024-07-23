import { Backdrop, CircularProgress, Paper, PaperProps } from '@mui/material';
import React, { FC } from 'react';

interface ModalProps extends PaperProps {
    open?: boolean;
    handleClose?: () => void;
    loading?: boolean;
}

const Modal: FC<ModalProps> = ({ open, handleClose, ...props }) => {
    return (
        <Backdrop
            sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.appBar - 1 }}
            open={open ?? false}
            onClick={handleClose}
        >
            {props.loading === true ? (
                <CircularProgress />
            ) : (
                <Paper onClick={(e) => e.stopPropagation()} elevation={24} {...props} />
            )}
        </Backdrop>
    );
};

export default Modal;
