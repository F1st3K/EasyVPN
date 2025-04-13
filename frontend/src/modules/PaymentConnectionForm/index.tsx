import { Box, TextField } from '@mui/material';
import React, { FC, useEffect, useState } from 'react';

import { PaymentConnectionInfo } from '../../api';

interface PaymentConnectionFormProps {
    paymentInfo?: PaymentConnectionInfo;
    onChange?: (paymentInfo: PaymentConnectionInfo) => void;
    readonlyDays?: boolean;
    readonlyDesc?: boolean;
    readonlyImages?: boolean;
}

const PaymentConnectionForm: FC<PaymentConnectionFormProps> = (props) => {
    const [days, setDays] = useState<number>(props.paymentInfo?.days ?? 30);
    const [desc, setDesc] = useState<string>(props.paymentInfo?.description ?? '');
    const [images] = useState<string[]>(props.paymentInfo?.images ?? []);

    useEffect(() => {
        props.onChange &&
            props.onChange({ days: days, description: desc, images: images });
    }, [days, desc, images]);

    return (
        <Box display="flex" flexDirection="column" gap={2}>
            <TextField
                disabled={props.readonlyDays}
                label="Count days"
                value={Number.isInteger(days) ? days : ''}
                onChange={(e) => {
                    const d = Number.parseInt(e.target.value);
                    return setDays(d > 0 ? d : 1);
                }}
                onFocus={(e) => e.target.select()}
                type="number"
                variant="outlined"
                inputMode="numeric"
                size="small"
                style={{ width: '15ch' }}
            />
            <TextField
                disabled={props.readonlyDesc}
                label="Comment"
                value={desc}
                onChange={(e) => setDesc(e.target.value)}
                inputMode="text"
                multiline
                rows={3}
            />
        </Box>
    );
};

export default PaymentConnectionForm;
