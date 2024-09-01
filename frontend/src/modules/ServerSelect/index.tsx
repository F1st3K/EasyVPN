import { Autocomplete, Box, TextField } from '@mui/material';
import React, { FC, useState } from 'react';

import EasyVpn, { ApiError, Server, VpnVersion } from '../../api';

interface ServerSelectProps {
    serverId?: string;
    onChange?: (server: Server | null) => void;
}

const ServerSelect: FC<ServerSelectProps> = (props) => {
    const [servers, setServers] = useState<Server[]>([
        {
            id: '00000000-0000-0000-0000-000000000000',
            protocol: {
                id: '00000000-0000-0000-0000-000000000000',
                name: 'WireGuard',
                icon: 'https://static-00.iconduck.com/assets.00/wireguard-icon-256x256-bdlmygje.png',
            },
            version: VpnVersion.V1,
        },
        {
            id: '10000000-0000-0000-0000-000000000000',
            protocol: {
                id: '00000000-0000-0000-0000-000000000000',
                name: 'vireGuard',
                icon: 'https://static-00.iconduck.com/assets.00/wireguard-icon-256x256-bdlmygje.png',
            },
            version: VpnVersion.V1,
        },
    ]);
    const [server, setServer] = useState<Server | null>(
        servers.find((s) => s.id == props.serverId) || null,
    );

    return (
        <Autocomplete
            sx={{ width: '23ch' }}
            autoHighlight
            options={servers}
            onChange={(_, s) => setServer(s)}
            value={server}
            getOptionLabel={(o) => o.protocol.name}
            renderOption={(p, o) => {
                const { id, ...op } = p;
                return (
                    <Box
                        key={id}
                        component="li"
                        sx={{ '& > img': { mr: 2, flexShrink: 0 } }}
                        {...op}
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
