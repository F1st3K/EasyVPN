import { Box, TextField } from '@mui/material';
import React, { FC, useEffect, useState } from 'react';

import { ProtocolInfo } from '../../api';

interface ProtocolInfoFormProps {
    protocol: ProtocolInfo;
    onChange?: (protocol: ProtocolInfo) => void;
}

const ProtocolInfoForm: FC<ProtocolInfoFormProps> = (props) => {
    const [name, setName] = useState<string>(props.protocol.name);
    const [icon, setIcon] = useState<string>(props.protocol.icon);

    useEffect(() => {
        setName(props.protocol.name);
        setIcon(props.protocol.icon);
    }, [props.protocol]);

    useEffect(() => {
        props.onChange && props.onChange({ name, icon });
    }, [name, icon]);

    return (
        <Box display="flex" flexDirection="column" gap={2}>
            <TextField
                label="Protocol name"
                value={name}
                onChange={(e) => setName(e.target.value)}
                onFocus={(e) => e.target.select()}
                type="text"
                variant="outlined"
                inputMode="text"
            />
            <TextField
                label="Icon url"
                value={icon}
                onChange={(e) => setIcon(e.target.value)}
                onFocus={(e) => e.target.select()}
                type="text"
                variant="outlined"
                inputMode="url"
            />
            <img
                style={{
                    maxWidth: '100%',
                    height: 'auto',
                    display: 'block',
                }}
                src={icon}
                alt=""
            />
        </Box>
    );
};

export default ProtocolInfoForm;
