import { Chip } from '@mui/material';
import { format } from 'date-fns';
import React, { FC } from 'react';

interface ExpireItemProps {
    ExpireTime: Date;
    WarrningDaysBefore: number;
}

const ExpireItem: FC<ExpireItemProps> = (props) => {
    const warrningTime = new Date(
        new Date(props.ExpireTime).setDate(
            props.ExpireTime.getDate() - props.WarrningDaysBefore,
        ),
    );
    return (
        <Chip
            label={
                <>
                    {new Date() > props.ExpireTime ? <>Expired</> : <>Expires</>}{' '}
                    {format(props.ExpireTime, 'dd.MM.yyyy') +
                        ' at ' +
                        format(props.ExpireTime, 'HH:mm')}
                </>
            }
            variant="outlined"
            color={
                new Date() > warrningTime
                    ? new Date() > props.ExpireTime
                        ? 'error'
                        : 'warning'
                    : 'info'
            }
            size="small"
        />
    );
};

export default ExpireItem;
