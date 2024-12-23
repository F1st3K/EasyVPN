import React, { FC, useEffect, useRef } from 'react';

export const HeaderSpace: FC = () => {
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

    return <div ref={divRef} />;
};

export default HeaderSpace;
