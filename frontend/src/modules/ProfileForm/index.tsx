import { Box, TextField } from '@mui/material';
import React, { FC, useEffect, useState } from 'react';

import { UserInfo } from '../../api';

interface ProfileFormProps {
    userInfo?: UserInfo;
    onChange?: (userInfo: UserInfo) => void;
}

const ProfileForm: FC<ProfileFormProps> = (props) => {
    const [icon, setIcon] = useState<string>(props.userInfo?.icon ?? '');
    const [firstName, setFirstName] = useState<string>(props.userInfo?.firstName ?? '');
    const [lastName, setLastName] = useState<string>(props.userInfo?.lastName ?? '');

    useEffect(() => {
        props.onChange && props.onChange({ firstName, lastName, icon });
    }, [icon, firstName, lastName]);

    return (
        <Box sx={{ width: '100%' }} display="flex" flexDirection="column" gap={2}>
            <TextField
                label="Icon url"
                value={icon}
                onChange={(e) => setIcon(e.target.value)}
            />
            <TextField
                label="First name"
                value={firstName}
                onChange={(e) => setFirstName(e.target.value)}
            />
            <TextField
                label="Last name"
                value={lastName}
                onChange={(e) => setLastName(e.target.value)}
            />
        </Box>
    );
};

export default ProfileForm;
