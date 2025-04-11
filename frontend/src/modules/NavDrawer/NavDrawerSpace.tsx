import React, { FC, useEffect, useRef } from 'react';

export const NavDrawerSpace: FC = () => {
    const divRef = useRef<HTMLDivElement | null>(null);
    useEffect(() => {
        // Функция для обновления высоты div
        const updateWidth = () => {
            const nav = document.querySelector('nav');
            const navWidth = nav ? nav.offsetWidth : 0;

            if (divRef.current) {
                divRef.current.style.width = `${navWidth}px`;
            }
        };

        // Первоначальная установка ширины при монтировании компонента
        updateWidth();

        // Добавляем слушатель события resize для обновления ширины при изменении окна
        window.addEventListener('resize', updateWidth);

        // Очистка слушателя события при размонтировании компонента
        return () => {
            window.removeEventListener('resize', updateWidth);
        };
    }, []);

    return <div ref={divRef} />;
};

export default NavDrawerSpace;
