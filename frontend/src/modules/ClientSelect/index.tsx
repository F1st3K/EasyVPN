import { Autocomplete, Box, TextField } from '@mui/material';
import React, { FC, useContext, useEffect } from 'react';

import { Context } from '../..';
import EasyVpn, { ApiError, User } from '../../api';
import { useRequest } from '../../hooks';

interface ClientSelectProps {
    clientId?: string;
    onChange?: (server: User | null) => void;
}

const ClientSelect: FC<ClientSelectProps> = (props) => {
    const { Auth } = useContext(Context);

    const [clients, loading] = useRequest<User[], ApiError>(() =>
        EasyVpn.users.getClients(Auth.getToken()).then((v) => v.data),
    );

    const client = clients?.find((u) => u.id == props.clientId) || null;

    useEffect(() => {
        loading == false && props.onChange && props.onChange(client);
    }, [client]);

    return (
        <Autocomplete
            autoHighlight
            options={clients ?? []}
            onChange={(_, s) => props.onChange && props.onChange(s)}
            value={client}
            getOptionLabel={(o) => o.firstName + ' ' + o.lastName}
            renderOption={(p, o) => {
                return (
                    <Box
                        component="li"
                        sx={{ '& > img': { mr: 2, flexShrink: 0 } }}
                        {...p}
                    >
                        <img loading="lazy" width={25} src={o.icon} alt="" />
                        {o.firstName + ' ' + o.lastName}
                    </Box>
                );
            }}
            renderInput={(params) => {
                return (
                    <Box
                        gap={1}
                        sx={{
                            display: 'flex',
                            alignItems: 'center',
                        }}
                    >
                        <img width={40} src={client?.icon} alt="" />
                        <TextField {...params} label="Choose client" />
                    </Box>
                );
            }}
        />
    );
};

export default ClientSelect;
