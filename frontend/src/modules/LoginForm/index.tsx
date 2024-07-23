import './style.css';

import LoadingButton from '@mui/lab/LoadingButton';
import { Alert, Box } from '@mui/material';
import TextField from '@mui/material/TextField';
import { observer } from 'mobx-react-lite';
import React, { FC, useContext, useState } from 'react';
import { useLocation } from 'react-router-dom';

import { Context } from '../..';
import { ApiError } from '../../api';
import SecretOutlinedField from '../../components/SecretOutlinedField';
import { useCustomNavigate } from '../../hooks';

const LoginForm: FC = () => {
    const { Auth } = useContext(Context);

    const customNavigate = useCustomNavigate();
    const { search } = useLocation();
    const searchParams = new URLSearchParams(search);

    const prevPage = searchParams.get('prevPage');

    const [login, setLogin] = useState<string>('');
    const [password, setPassword] = useState<string>('');

    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<ApiError | null>(null);

    function loginHandler() {
        setLoading(true);
        Auth.login(login, password)
            .then(() => {
                customNavigate(prevPage ?? '/');
            })
            .catch((e) => {
                setError(e);
                console.log(e);
            })
            .finally(() => {
                setLoading(false);
            });
    }

    return (
        <Box className="login-form">
            <TextField
                sx={{ width: '25ch', my: 1 }}
                label="Login"
                variant="outlined"
                onChange={(e) => setLogin(e.target.value)}
                value={login}
            />
            <SecretOutlinedField
                sx={{ width: '25ch', my: 1 }}
                label="Password"
                onChange={(e) => setPassword(e.target.value)}
                value={password}
            />
            <LoadingButton
                sx={{ my: 1 }}
                variant="contained"
                size="large"
                onClick={loginHandler}
                loading={loading}
            >
                Sign In
            </LoadingButton>
            {error ? (
                <Alert severity="error" variant="outlined" sx={{ width: '25ch' }}>
                    {error.response?.data.title ?? error.message}
                </Alert>
            ) : null}
        </Box>
    );
};

export default observer(LoginForm);
