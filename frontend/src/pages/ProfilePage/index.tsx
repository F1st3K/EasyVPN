import { Logout } from '@mui/icons-material';
import {
    Alert,
    Autocomplete,
    Box,
    Button,
    LinearProgress,
    Paper,
    TextField,
    Typography,
} from '@mui/material';
import { observer } from 'mobx-react-lite';
import React from 'react';
import { FC, useContext } from 'react';

import { Context } from '../..';
import EasyVpn, { ApiError, Role, User } from '../../api';
import CenterBox from '../../components/CenterBox';
import SecretOutlinedField from '../../components/SecretOutlinedField';
import { useRequest } from '../../hooks';

const ProfilePage: FC = () => {
    const { Auth } = useContext(Context);

    const [data, loading, error] = useRequest<User, ApiError>(
        () => EasyVpn.my.profile(Auth.getToken()).then((v) => v.data),
        [location.pathname],
    );

    if (loading) return <LinearProgress />;
    return (
        <CenterBox margin={2}>
            {error ? (
                <Alert severity="error" variant="outlined">
                    {error.response?.data.title ?? error.message}
                </Alert>
            ) : (
                <Paper
                    sx={{
                        borderRadius: 2,
                        display: 'flex',
                        flexDirection: { xs: 'column', md: 'row' },
                        width: '100%',
                        overflow: 'hidden',
                    }}
                >
                    <Box
                        sx={{
                            padding: '30px',
                            width: { xs: '100%', md: '35ch' },
                            display: 'flex',
                            flexDirection: 'column',
                            alignItems: 'center',
                        }}
                    >
                        <Typography variant="h4" padding={3}>
                            Your profile:
                        </Typography>
                        <Box
                            component="img"
                            src={data?.icon}
                            sx={{
                                width: '30ch',
                                height: '30ch',
                                borderRadius: '50%',
                                objectFit: 'cover',
                            }}
                        />
                        <Typography variant="h5" paddingTop={3}>
                            {data?.firstName} {data?.lastName}
                        </Typography>
                    </Box>

                    <Paper
                        elevation={4}
                        sx={{
                            borderRadius: 2,
                            flex: 1,
                            display: 'flex',
                            flexWrap: 'wrap',
                            padding: 5,
                            justifyContent: 'center',
                            gap: 5,
                        }}
                    >
                        <Box
                            sx={{
                                width: { xs: '100%', md: '35ch' },
                                display: 'flex',
                                flexDirection: 'column',
                            }}
                        >
                            <Autocomplete
                                disabled
                                multiple
                                options={Object.values(Role)}
                                renderInput={(params) => (
                                    <TextField {...params} label="Roles" />
                                )}
                                value={data?.roles}
                            />
                        </Box>
                        <Box
                            sx={{
                                width: { xs: '100%', md: '35ch' },
                                display: 'flex',
                                flexDirection: 'column',
                                gap: 5,
                            }}
                        >
                            <TextField label="Login" value={data?.login} />
                            <SecretOutlinedField label="Password" value="*************" />
                            <Box flexGrow={1} />
                            <Button
                                endIcon={<Logout />}
                                color="error"
                                variant="outlined"
                                onClick={() => Auth.logout()}
                            >
                                Log out
                            </Button>
                        </Box>
                    </Paper>
                </Paper>
            )}
        </CenterBox>
    );
};

export default observer(ProfilePage);
