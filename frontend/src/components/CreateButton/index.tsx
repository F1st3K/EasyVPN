import { Add } from '@mui/icons-material';
import { BoxProps, Button } from '@mui/material';
import React, { FC, ReactNode } from 'react';

import CenterBox from '../CenterBox';

interface CreateButtonProps extends BoxProps {
    description?: ReactNode;
    onClick?: () => void;
}

const CreateButton: FC<CreateButtonProps> = (props: CreateButtonProps) => {
    return (
        <CenterBox {...props} sx={{ width: '100%' }}>
            <Button
                variant="outlined"
                sx={{
                    whiteSpace: 'normal',
                    textTransform: 'none',
                }}
                fullWidth
                onClick={props.onClick}
            >
                <CenterBox>
                    {props.description}
                    <Add fontSize="large" />
                </CenterBox>
            </Button>
        </CenterBox>
    );
};

export default CreateButton;
