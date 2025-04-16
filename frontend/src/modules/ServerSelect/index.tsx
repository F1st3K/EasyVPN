import { Autocomplete, Box, TextField } from '@mui/material';
import React, { FC, useContext, useEffect } from 'react';

import { Context } from '../..';
import EasyVpn, { ApiError, Server } from '../../api';
import { useRequest } from '../../hooks';

interface ServerSelectProps {
    serverId?: string;
    onChange?: (server: Server | null) => void;
}

const ServerSelect: FC<ServerSelectProps> = (props) => {
    const { Auth } = useContext(Context);

    const [servers, loading] = useRequest<Server[], ApiError>(() =>
        EasyVpn.servers.getAll(Auth.getToken()).then((v) => v.data),
    );

    const server = servers?.find((s) => s.id == props.serverId) || null;

    useEffect(() => {
        loading == false && props.onChange && props.onChange(server);
    }, [server]);

    return (
        <Autocomplete
            sx={{ width: '23ch' }}
            autoHighlight
            options={servers ?? []}
            onChange={(_, s) => props.onChange && props.onChange(s)}
            value={server}
            getOptionLabel={(o) => o.protocol.name}
            renderOption={(p, o) => {
                return (
                    <Box
                        component="li"
                        sx={{ '& > img': { mr: 2, flexShrink: 0 } }}
                        {...p}
                    >
                        <img loading="lazy" width={25} src={o.protocol.icon} alt="" />
                        {o.protocol.name}
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
                        <img width={40} src={server?.protocol.icon} alt="" />
                        <TextField {...params} label="Choose a server" />
                    </Box>
                );
            }}
        />
    );
};

export default ServerSelect;
