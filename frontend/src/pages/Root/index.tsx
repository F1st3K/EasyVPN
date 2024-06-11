import { Container } from '@mui/material';
import React from 'react';
import { FC } from 'react';
import { Outlet } from 'react-router-dom';

import Footer from '../../modules/Footer';
import Header from '../../modules/Header';

const Root: FC = () => {
    return (
        <>
            <Header />
            <Container
                component="main"
                maxWidth="xs"
                sx={{
                    display: 'flex',
                    justifyContent: 'center',
                    alignItems: 'center',
                }}
            >
                <Outlet />
            </Container>
            <Footer />
        </>
    );
};

export default Root;
