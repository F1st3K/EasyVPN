import { ContentCopy, DoneAll } from '@mui/icons-material';
import { IconButton, IconButtonProps } from '@mui/material';
import React, { FC, useState } from 'react';

interface CopyButtonProps extends IconButtonProps {
    text: string;
}

const CopyButton: FC<CopyButtonProps> = (props) => {
    const [isCopied, setIsCopied] = useState(false);

    const handleCopy = () => {
        navigator.clipboard.writeText(props.text);
        setIsCopied(true);
        const interval = setInterval(() => {
            clearInterval(interval);
            setIsCopied(false);
        }, 10 * 1000);
    };

    return (
        <IconButton onClick={() => handleCopy()} {...props}>
            {isCopied ? <DoneAll /> : <ContentCopy />}
        </IconButton>
    );
};

export default CopyButton;
