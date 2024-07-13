import { Box, BoxProps } from '@mui/material';
import React, { FC } from 'react';

const CenterBox: FC<BoxProps> = (props: BoxProps) => {
    return (
        <Box
            sx={{
                alignItems: 'center',
                justifyContent: 'center',
                display: 'flex',
                flexDirection: 'column',
            }}
        >
            <Box {...props} />
        </Box>
    );
};

export default CenterBox;
