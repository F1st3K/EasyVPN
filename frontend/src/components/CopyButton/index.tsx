import { ContentCopy, DoneAll } from '@mui/icons-material';
import { IconButton } from '@mui/material';
import React, { FC, useState } from 'react';

interface CopyButtonProps {
    text: string;
}

const CopyButton: FC<CopyButtonProps> = (props) => {
    const [isCopied, setIsCopied] = useState(false);

    const handleCopy = () => {
        navigator.clipboard.writeText(props.text);
        setIsCopied(true);
        const interval = setInterval(() => {
            setIsCopied(false);
            clearInterval(interval);
        }, 10 * 1000);
    };

    return (
        <IconButton onClick={handleCopy}>
            {isCopied ? <DoneAll /> : <ContentCopy />}
        </IconButton>
    );
};

export default CopyButton;
