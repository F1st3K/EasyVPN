import './style.css';

import { LoadingButton } from '@mui/lab';
import { Alert, Box, TextField } from '@mui/material';
import { observer } from 'mobx-react-lite';
import React, { FC, useContext, useState } from 'react';
import { useLocation } from 'react-router-dom';

import { Context } from '../..';
import { ApiError } from '../../api';
import SecretOutlinedField from '../../components/SecretOutlinedField';
import { useCustomNavigate } from '../../hooks';

const RegisterForm: FC = () => {
    const [firstName, setFirstName] = useState<string>('');
    const [lastName, setLastName] = useState<string>('');
    const [login, setLogin] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [remPassword, setRemPassword] = useState<string>('');
    const store = useContext(Context);

    const customNavigate = useCustomNavigate();
    const { search } = useLocation();
    const searchParams = new URLSearchParams(search);

    const prevPage = searchParams.get('prevPage');

    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<ApiError | null>(null);

    function registerHandler() {
        setLoading(true);
        store.Auth.register({ firstName, lastName, login, password })
            .then(() => customNavigate(prevPage ?? '/'))
            .catch((e) => {
                setError(e);
                console.log(e);
            })
            .finally(() => setLoading(false));
    }

    return (
        <Box className="register-form">
            <TextField
                sx={{ width: '25ch' }}
                label="First name"
                variant="outlined"
                onChange={(e) => setFirstName(e.target.value)}
                value={firstName}
            />
            <TextField
                sx={{ width: '25ch', my: 1 }}
                label="Last name"
                variant="outlined"
                onChange={(e) => setLastName(e.target.value)}
                value={lastName}
            />
            <TextField
                sx={{ width: '25ch' }}
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
            <SecretOutlinedField
                sx={{ width: '25ch' }}
                error={remPassword !== password}
                label="Repit password"
                onChange={(e) => setRemPassword(e.target.value)}
                value={remPassword}
            />
            <LoadingButton
                sx={{ my: 1 }}
                disabled={remPassword !== password}
                variant="contained"
                size="large"
                onClick={registerHandler}
                loading={loading}
            >
                Sign Up
            </LoadingButton>
            {error ? (
                <Alert severity="error" sx={{ width: '25ch' }}>
                    {error.response?.data.title ?? error.message}
                </Alert>
            ) : null}
        </Box>
    );
};

export default observer(RegisterForm);
