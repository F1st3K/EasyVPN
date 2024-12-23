import { Box, CssBaseline, Drawer } from '@mui/material';
import React, { useEffect, useState } from 'react';

import { HeaderSpace } from '../Header';
import ResponsivePageList from '../ResponsivePageList';

export const NavDrawer = (props: {
    open: boolean;
    onClose: () => void;
    isMobile: () => boolean;
    children?: React.ReactNode;
}) => {
    const [isClosing, setIsClosing] = useState(props.isMobile());

    const handleDrawerClose = () => {
        setIsClosing(true);
        props.onClose();
    };

    useEffect(() => {
        setIsClosing(!props.open);
    }, [props.open]);

    return (
        <Box component="nav" aria-label="mailbox folders">
            <CssBaseline />
            <Drawer
                variant={props.isMobile() ? 'temporary' : 'persistent'}
                sx={{
                    width: '30ch',
                    flexShrink: 0,
                    [`& .MuiDrawer-paper`]: {
                        width: '30ch',
                        boxSizing: 'border-box',
                    },
                }}
                open={!isClosing || !props.isMobile()}
                onClose={handleDrawerClose}
                ModalProps={{
                    keepMounted: props.isMobile(), // better open performance on mobile.
                }}
            >
                <HeaderSpace />
                <ResponsivePageList onNavigate={handleDrawerClose} />
            </Drawer>
        </Box>
    );
};

export default NavDrawer;
