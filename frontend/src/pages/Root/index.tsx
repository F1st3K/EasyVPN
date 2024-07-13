import { Box } from '@mui/material';
import React from 'react';
import { FC } from 'react';
import { Outlet } from 'react-router-dom';

import Footer from '../../modules/Footer';
import Header from '../../modules/Header';

const Root: FC = () => {
    return (
        <>
            <Header />
            <Box component="main">
                <Outlet />
            </Box>
            <Footer />
        </>
    );
};

export default Root;
