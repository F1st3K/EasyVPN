import { Menu } from '@mui/material';
import React, { FC } from 'react';

interface PopUpMenuProps {
    anchorEl?: HTMLElement | null;
    children?: React.ReactNode;
    onClose?: (event: Event, reason: 'backdropClick' | 'escapeKeyDown') => void;
}

const PopUpMenu: FC<PopUpMenuProps> = (props: PopUpMenuProps) => {
    return (
        <Menu
            id="menu-appbar"
            anchorEl={props.anchorEl}
            anchorOrigin={{
                vertical: 'bottom',
                horizontal: 'left',
            }}
            keepMounted
            transformOrigin={{
                vertical: 'top',
                horizontal: 'left',
            }}
            open={Boolean(props.anchorEl)}
            onClose={props.onClose}
        >
            {props.children}
        </Menu>
    );
};

export default PopUpMenu;
