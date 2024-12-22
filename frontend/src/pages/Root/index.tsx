import { Box, styled } from '@mui/material';
import React, { useEffect, useRef, useState } from 'react';
import { FC } from 'react';
import { Outlet } from 'react-router-dom';

import Footer from '../../modules/Footer';
import Header from '../../modules/Header';
import ResponsiveDrawer from '../../modules/ResponsiveDrawer';

const Root: FC = () => {
    const isMobile = () => window.innerWidth <= 768;
    const [navIsOpen, setNavIsOpen] = useState(!isMobile());

    const divRef = useRef<HTMLDivElement | null>(null);
    useEffect(() => {
        // Функция для обновления высоты div
        const updateHeight = () => {
            const header = document.querySelector('header');
            const headerHeight = header ? header.offsetHeight : 0;

            if (divRef.current) {
                divRef.current.style.height = `${headerHeight}px`;
            }
        };

        // Первоначальная установка высоты при монтировании компонента
        updateHeight();

        // Добавляем слушатель события resize для обновления высоты при изменении окна
        window.addEventListener('resize', updateHeight);

        // Очистка слушателя события при размонтировании компонента
        return () => {
            window.removeEventListener('resize', updateHeight);
        };
    }, []);

    return (
        <>
            <Header isMobile={isMobile} toggleNav={() => setNavIsOpen((x) => !x)} />
            <Box display="flex">
                <ResponsiveDrawer
                    open={navIsOpen}
                    onClose={() => setNavIsOpen(false)}
                    isMobile={isMobile}
                />
                <Box flexDirection="column">
                    <div ref={divRef} />
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
