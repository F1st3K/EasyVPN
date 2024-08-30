import { TextField } from '@mui/material';
import React, { FC, useEffect, useState } from 'react';

import { PaymentConnectionInfo } from '../../api';
import CenterBox from '../../components/CenterBox';

interface PaymentConnectionFormProps {
    paymentInfo?: PaymentConnectionInfo;
    onChange?: (paymentInfo: PaymentConnectionInfo) => void;
}

const PaymentConnectionForm: FC<PaymentConnectionFormProps> = (props) => {
    const [days, setDays] = useState<number>(props.paymentInfo?.days ?? 30);
    const [desc, setDesc] = useState<string>(props.paymentInfo?.description ?? '');
    const [images, setImages] = useState<string[]>(props.paymentInfo?.images ?? []);

    useEffect(() => {
        props.onChange &&
            props.onChange({ days: days, description: desc, images: images });
    }, [days, desc, images]);

    return (
        <CenterBox flexDirection={'column'}>
            <TextField
                label="Count days"
                value={Number.isInteger(days) ? days : ''}
                onChange={(e) => {
                    const d = Number.parseInt(e.target.value);
                    return setDays(Number.isNaN(d) || d > 0 ? d : 1);
                }}
                type="number"
                variant="outlined"
                inputMode="numeric"
            />
            <TextField
                label="Comment"
                value={desc}
                onChange={(e) => setDesc(e.target.value)}
                inputMode="text"
                multiline
            />
        </CenterBox>
    );
};

export default PaymentConnectionForm;
