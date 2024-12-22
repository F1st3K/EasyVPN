import { Inbox, Mail } from '@mui/icons-material';
import {
    Box,
    CssBaseline,
    Divider,
    Drawer,
    List,
    ListItem,
    ListItemButton,
    ListItemIcon,
    ListItemText,
    Toolbar,
} from '@mui/material';
import React, { useEffect, useState } from 'react';

export const ResponsiveDrawer = (props: {
    open: boolean;
    onClose: () => void;
    isMobile: () => boolean;
}) => {
    const [isClosing, setIsClosing] = useState(props.isMobile());

    const handleDrawerClose = () => {
        setIsClosing(true);
        props.onClose();
    };

    useEffect(() => {
        setIsClosing(!props.open);
    }, [props.open]);

    const drawer = (
        <div>
            <Toolbar />
            <Divider />
            <List>
                {['Inbox', 'Starred', 'Send email', 'Drafts'].map((text, index) => (
                    <ListItem key={text} disablePadding>
                        <ListItemButton onClick={() => handleDrawerClose()}>
                            <ListItemIcon>
                                {index % 2 === 0 ? <Inbox /> : <Mail />}
                            </ListItemIcon>
                            <ListItemText primary={text} />
                        </ListItemButton>
                    </ListItem>
                ))}
            </List>
            <Divider />
            <List>
                {['All mail', 'Trash', 'Spam'].map((text, index) => (
                    <ListItem key={text} disablePadding>
                        <ListItemButton>
                            <ListItemIcon>
                                {index % 2 === 0 ? <Inbox /> : <Mail />}
                            </ListItemIcon>
                            <ListItemText primary={text} />
                        </ListItemButton>
                    </ListItem>
                ))}
            </List>
        </div>
    );

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
                {drawer}
            </Drawer>
        </Box>
    );
};

export default ResponsiveDrawer;
