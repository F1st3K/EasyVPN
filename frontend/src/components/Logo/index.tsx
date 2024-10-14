import { Box, BoxProps, useTheme } from '@mui/material';
import React, { FC } from 'react';

interface LogoProps extends BoxProps {
    size?: number;
    theme?: 'dark' | 'light';
}

const Logo: FC<LogoProps> = (props) => {
    const theme = useTheme();

    return (
        <Box
            sx={{
                alignItems: 'center',
                justifyContent: 'center',
                display: 'flex',
            }}
            {...props}
            width={props.size || 25}
        >
            <img
                loading="eager"
                width="100%"
                height="100%"
                src={`/EasyVPN-logo.${props.theme || theme.palette.mode}.png`}
                alt=""
            />
        </Box>
    );
};

export default Logo;
