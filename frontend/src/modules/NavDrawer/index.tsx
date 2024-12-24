import { Box, CssBaseline, Drawer } from '@mui/material';
import React, { useEffect, useState } from 'react';

import { HeaderSpace } from '../Header';
import ResponsivePageList from '../ResponsivePageList';
import NavDrawerSpace from './NavDrawerSpace';

export const NavDrawer = (props: {
    width: string;
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
                variant={props.isMobile() ? 'temporary' : 'permanent'}
                sx={{
                    width: props.width,
                    flexShrink: 0,
                    [`& .MuiDrawer-paper`]: {
                        width: props.width,
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
export { NavDrawerSpace };
