import { Box } from '@mui/material';
import React, { useState } from 'react';
import { FC } from 'react';
import { Outlet } from 'react-router-dom';

import Footer from '../../modules/Footer';
import Header from '../../modules/Header';
import ResponsiveDrawer from '../../modules/ResponsiveDrawer';

const Root: FC = () => {
    const isMobile = () => window.innerWidth <= 768;
    const [navIsOpen, setNavIsOpen] = useState(!isMobile());

    return (
        <>
            <Header isMobile={isMobile} toggleNav={() => setNavIsOpen((x) => !x)} />
            <Box display="flex">
                <ResponsiveDrawer
                    open={navIsOpen}
                    onClose={() => setNavIsOpen(false)}
                    isMobile={isMobile}
                />
                <Box component="main">
                    <Outlet />
                </Box>
            </Box>
            <Footer />
        </>
    );
};

export default Root;
