import { Close, Done, Edit, Logout } from '@mui/icons-material';
import {
    Alert,
    Autocomplete,
    Box,
    Button,
    CircularProgress,
    IconButton,
    LinearProgress,
    Paper,
    TextField,
    Typography,
} from '@mui/material';
import { observer } from 'mobx-react-lite';
import React, { useEffect, useState } from 'react';
import { FC, useContext } from 'react';

import { Context } from '../..';
import EasyVpn, { ApiError, Role, User, UserInfo } from '../../api';
import CenterBox from '../../components/CenterBox';
import SecretOutlinedField from '../../components/SecretOutlinedField';
import { useRequest, useRequestHandler } from '../../hooks';
import ProfileForm from '../../modules/ProfileForm';

const ProfilePage: FC = () => {
    const { Auth } = useContext(Context);

    const [data, loading, error] = useRequest<User, ApiError>(
        () => EasyVpn.my.profile(Auth.getToken()).then((v) => v.data),
        [location.pathname],
    );
    useEffect(() => {
        setProfile({
            firstName: data?.firstName || '',
            lastName: data?.lastName || '',
            icon: data?.icon || '',
        });
        setLogin(data?.login || '');
    }, [data]);

    const [profile, setProfile] = useState<UserInfo>({
        firstName: '',
        lastName: '',
        icon: '',
    });
    const [profileDisabled, setProfileDisabled] = useState(true);
    const [profileHandler, loadingPf, errorPf] = useRequestHandler<void, ApiError>(() =>
        EasyVpn.my.editProfile(profile, Auth.getToken()).then((r) => r.data),
    );

    const [login, setLogin] = useState('');
    const [loginDisabled, setLoginDisabled] = useState(true);
    const [loginHandler, loadingLg, errorLg] = useRequestHandler<void, ApiError>(() =>
        EasyVpn.my.updateLogin(login, Auth.getToken()).then((r) => r.data),
    );

    const [pwd, setPwd] = useState('************');
    const [newPwd, setNewPwd] = useState('');
    const [pwdDisabled, setPwdDisabled] = useState(true);
    const [pwdHandler, loadingPwd, errorPwd] = useRequestHandler<void, ApiError>(() =>
        EasyVpn.my.updatePassword(pwd, newPwd, Auth.getToken()).then((r) => r.data),
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
                        <Box width="100%" display="flex" justifyContent="right">
                            <IconButton onClick={() => setProfileDisabled((x) => !x)}>
                                {profileDisabled ? <Edit /> : <Close />}
                            </IconButton>
                            {!profileDisabled && (
                                <IconButton
                                    onClick={() =>
                                        profileHandler(null, () =>
                                            setProfileDisabled(true),
                                        )
                                    }
                                >
                                    <Done />
                                </IconButton>
                            )}
                        </Box>
                        <Box
                            component="img"
                            src={profile.icon}
                            sx={{
                                width: '30ch',
                                height: '30ch',
                                borderRadius: '50%',
                                objectFit: 'cover',
                                marginBottom: 3,
                            }}
                        />
                        {profileDisabled ? (
                            <Typography variant="h4">
                                {profile.firstName} {profile.lastName}
                            </Typography>
                        ) : (
                            <ProfileForm userInfo={profile} onChange={setProfile} />
                        )}
                        {loadingPf && <CircularProgress />}
                        {errorPf && (
                            <Alert severity="error" variant="outlined">
                                {errorPf.response?.data.title ?? errorPf.message}
                            </Alert>
                        )}
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
                            <Box display="flex" alignItems="center">
                                <TextField
                                    sx={{ width: '100%' }}
                                    label="Login"
                                    value={login}
                                    onChange={(e) => setLogin(e.target.value)}
                                    disabled={loginDisabled}
                                />
                                <IconButton onClick={() => setLoginDisabled((x) => !x)}>
                                    {loginDisabled ? <Edit /> : <Close />}
                                </IconButton>
                                {!loginDisabled && (
                                    <IconButton
                                        onClick={() =>
                                            loginHandler(null, () =>
                                                setLoginDisabled(true),
                                            )
                                        }
                                    >
                                        <Done />
                                    </IconButton>
                                )}
                            </Box>
                            {loadingLg && <CircularProgress />}
                            {errorLg && (
                                <Alert severity="error" variant="outlined">
                                    {errorLg.response?.data.title ?? errorLg.message}
                                </Alert>
                            )}
                            <Box display="flex" alignItems="center">
                                <SecretOutlinedField
                                    sx={{ width: '100%' }}
                                    label="Password"
                                    value={pwd}
                                    onChange={(e) => setPwd(e.target.value)}
                                    disabled={pwdDisabled}
                                />
                                <IconButton
                                    onClick={() => {
                                        setPwd(pwdDisabled ? '' : '************');
                                        setNewPwd('');
                                        setPwdDisabled((x) => !x);
                                    }}
                                >
                                    {pwdDisabled ? <Edit /> : <Close />}
                                </IconButton>
                                {!pwdDisabled && (
                                    <IconButton
                                        onClick={() =>
                                            pwdHandler(null, () => {
                                                setPwd('************');
                                                setNewPwd('');
                                                setPwdDisabled(true);
                                            })
                                        }
                                    >
                                        <Done />
                                    </IconButton>
                                )}
                            </Box>
                            {!pwdDisabled && (
                                <SecretOutlinedField
                                    sx={{ width: '100%' }}
                                    label="New password"
                                    value={newPwd}
                                    onChange={(e) => setNewPwd(e.target.value)}
                                    disabled={pwdDisabled}
                                />
                            )}
                            {loadingPwd && <CircularProgress />}
                            {errorPwd && (
                                <Alert severity="error" variant="outlined">
                                    {errorPwd.response?.data.title ?? errorPwd.message}
                                </Alert>
                            )}
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
