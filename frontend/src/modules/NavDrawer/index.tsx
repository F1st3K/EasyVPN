import { NoteAdd } from '@mui/icons-material';
import { Box, CssBaseline, Drawer, Fab } from '@mui/material';
import { observer } from 'mobx-react-lite';
import React, { useContext, useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

import { Context } from '../..';
import { Role } from '../../api';
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
    const { Auth } = useContext(Context);
    const { Pages } = useContext(Context);
    const [isClosing, setIsClosing] = useState(props.isMobile());

    const handleDrawerClose = () => {
        setIsClosing(true);
        props.onClose();
    };

    useEffect(() => {
        setIsClosing(!props.open);
    }, [props.open]);

    const navigate = useNavigate();

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
                <ResponsivePageList
                    topLevelIsOpen
                    parentRoute="pages"
                    routes={Pages.getPageRoutes()}
                    onNavigate={(r) => {
                        handleDrawerClose();
                        navigate(r);
                    }}
                />
                {Auth.roles.includes(Role.PageModerator) && (
                    <Fab
                        sx={{ m: 2 }}
                        variant="extended"
                        color="primary"
                        onClick={() => navigate('/pages/new')}
                    >
                        <NoteAdd sx={{ mr: 1 }} />
                        Add page
                    </Fab>
                )}
            </Drawer>
        </Box>
    );
};

export default observer(NavDrawer);
export { NavDrawerSpace };
