import { Box, CssBaseline, Drawer } from '@mui/material';
import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';

import EasyVpn, { ApiError } from '../../api';
import PageInfo from '../../api/responses/PageInfo';
import { useRequest } from '../../hooks';
import { HeaderSpace } from '../Header';
import ResponsivePageList, { PageRoutes } from '../ResponsivePageList';
import NavDrawerSpace from './NavDrawerSpace';

function buildRoutes(pages: PageInfo[]): PageRoutes[] {
    const result: PageRoutes[] = [];

    for (const page of pages) {
        const parts = page.route.split('/');
        let currentLevel = result;

        for (const part of parts) {
            // Найдем существующий маршрут или создадим новый
            let existingRoute = currentLevel.find((route) => route.page.route === part);

            if (!existingRoute) {
                existingRoute = {
                    page: { ...page, route: part }, // Сохраняем информацию о странице
                    childrens: [],
                };
                currentLevel.push(existingRoute);
            }
            currentLevel = existingRoute.childrens;
        }
    }

    return result;
}

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

    const [pages, loading, error] = useRequest<PageRoutes[], ApiError>(() =>
        EasyVpn.pages.getAll().then((v) => buildRoutes(v.data)),
    );
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
                {pages != null && (
                    <ResponsivePageList
                        parentRoute="pages"
                        routes={pages}
                        onNavigate={(r) => {
                            handleDrawerClose();
                            navigate(r);
                        }}
                    />
                )}
            </Drawer>
        </Box>
    );
};

export default NavDrawer;
export { NavDrawerSpace };
