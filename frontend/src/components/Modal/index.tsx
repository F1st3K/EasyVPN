import { Backdrop, Box, CircularProgress, Paper, PaperProps } from '@mui/material';
import React, { FC } from 'react';

import { HeaderSpace } from '../../modules/Header';
import { NavDrawerSpace } from '../../modules/NavDrawer';

interface ModalProps extends PaperProps {
    open?: boolean;
    handleClose?: () => void;
    loading?: boolean;
}

const Modal: FC<ModalProps> = ({ open, handleClose, ...props }) => {
    return (
        <>
            <Backdrop
                sx={{
                    zIndex: (theme) => theme.zIndex.appBar - 1,
                }}
                color="#fff"
                open={open ?? false}
                onClick={handleClose}
            >
                <Box>
                    <HeaderSpace />
                    <NavDrawerSpace />
                </Box>
                {props.loading === true ? (
                    <CircularProgress />
                ) : (
                    <Paper
                        style={{ margin: '15px', borderRadius: '15px' }}
                        onClick={(e) => e.stopPropagation()}
                        elevation={24}
                        {...props}
                    />
                )}
            </Backdrop>
        </>
    );
};

export default Modal;
