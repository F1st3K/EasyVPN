import { LoadingButton } from '@mui/lab';
import { Alert, Autocomplete, Box, TextField } from '@mui/material';
import React, { FC, useContext, useEffect, useState } from 'react';

import { Context } from '../..';
import EasyVpn, { ApiError, Protocol, ServerInfo, VpnVersion } from '../../api';
import SecretOutlinedField from '../../components/SecretOutlinedField';
import { useRequest, useRequestHandler } from '../../hooks';

interface ServerInfoFormProps {
    server: ServerInfo;
    onChange?: (server: ServerInfo) => void;
}

const ServerInfoForm: FC<ServerInfoFormProps> = (props) => {
    const { Auth } = useContext(Context);

    const [protocolId, setProtocolId] = useState<string>(props.server.protocolId);
    const [version, setVersion] = useState<VpnVersion>(props.server.version);
    const [auth, setAuth] = useState<string>(props.server.connection.auth);
    const [endpoint, setEndpoint] = useState<string>(props.server.connection.endpoint);

    useEffect(() => {
        setProtocolId(props.server.protocolId);
        setVersion(props.server.version);
    }, [props.server]);

    useEffect(() => {
        props.onChange &&
            props.onChange({ protocolId, version, connection: { auth, endpoint } });
    }, [protocolId, version, auth, endpoint]);

    const [protocols] = useRequest<Protocol[], ApiError>(() =>
        EasyVpn.protocols.getAll(Auth.getToken()).then((v) => v.data),
    );
    const protocol = protocols?.find((p) => p.id === protocolId);

    const [handleTest, loadingTest, errorTest] = useRequestHandler<void, ApiError>(() =>
        EasyVpn.servers
            .test(version, { auth, endpoint }, Auth.getToken())
            .then((v) => v.data),
    );

    return (
        <Box display="flex" flexDirection="column" gap={2}>
            <Box display="flex" flexDirection="row" gap={2}>
                <Autocomplete
                    disablePortal
                    options={Object.values(VpnVersion)}
                    value={version}
                    onChange={(_, v) => v && setVersion(v)}
                    style={{ width: '15ch' }}
                    renderInput={(params) => (
                        <TextField {...params} label="VPN API Version" />
                    )}
                />
                <LoadingButton
                    onClick={() => handleTest && handleTest(null)}
                    loading={loadingTest}
                    disabled={auth === '' || endpoint === ''}
                    variant="outlined"
                >
                    Test connection
                </LoadingButton>
            </Box>
            <TextField
                label="Endpoint"
                value={endpoint}
                onChange={(e) => setEndpoint(e.target.value)}
                onFocus={(e) => e.target.select()}
                type="text"
                variant="outlined"
                inputMode="url"
            />
            <SecretOutlinedField
                label="Auth"
                value={auth}
                onChange={(e) => setAuth(e.target.value)}
                onFocus={(e) => e.target.select()}
            />
            {errorTest && (
                <Alert severity="error" variant="outlined">
                    {errorTest.response?.data.title ?? errorTest.message}
                </Alert>
            )}
            <Autocomplete
                autoHighlight
                options={protocols ?? []}
                onChange={(_, p) => {
                    p && setProtocolId(p.id);
                }}
                value={protocol}
                getOptionLabel={(o) => o.name}
                renderOption={(p, o) => {
                    return (
                        <Box
                            component="li"
                            sx={{ '& > img': { mr: 2, flexShrink: 0 } }}
                            {...p}
                        >
                            <img loading="lazy" width={25} src={o.icon} alt="" />
                            {o.name}
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
                            <img width={40} src={protocol?.icon} alt="" />
                            <TextField label="Choose a protocol" {...params} />
                        </Box>
                    );
                }}
            />
        </Box>
    );
};

export default ServerInfoForm;
