import { Box } from '@mui/material';
import React, { useState } from 'react';
import { FC } from 'react';
import { Outlet } from 'react-router-dom';

import Footer from '../../modules/Footer';
import Header, { HeaderSpace } from '../../modules/Header';
import NavDrawer from '../../modules/NavDrawer';

const Root: FC = () => {
    const isMobile = () => window.innerWidth <= 768;
    const [navIsOpen, setNavIsOpen] = useState(!isMobile());

    return (
        <>
            <Header isMobile={isMobile} toggleNav={() => setNavIsOpen((x) => !x)} />
            <Box display="flex">
                <NavDrawer
                    width="20ch"
                    open={navIsOpen}
                    onClose={() => setNavIsOpen(false)}
                    isMobile={isMobile}
                />
                <Box
                    flexDirection="column"
                    width={isMobile() ? '100%' : 'calc(100% - 20ch)'}
                >
                    <HeaderSpace />
                    <Box component="main">
                        <Outlet />
                    </Box>
                    <Footer />
                </Box>
            </Box>
        </>
    );
};

export default Root;
