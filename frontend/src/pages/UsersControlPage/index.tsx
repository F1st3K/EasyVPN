import { Alert, Box, LinearProgress, Paper } from '@mui/material';
import { Buffer } from 'buffer';
import React, { useContext } from 'react';
import { FC } from 'react';
import { useNavigate } from 'react-router-dom';

import { Context } from '../..';
import EasyVpn, { ApiError, Role } from '../../api';
import Page from '../../api/requests/Page';
import { useRequestHandler } from '../../hooks';
import MarkDownX from '../../modules/MarkDownX';

const UsersControlPage: FC = () => {
    const navigate = useNavigate();
    const { Auth } = useContext(Context);
    const { Pages } = useContext(Context);

    const [createHandler, loading, error] = useRequestHandler<void, ApiError, Page>(
        (params) => EasyVpn.pages.create(params, Auth.getToken()).then((r) => r.data),
    );

    return (
        <>
            {loading && <LinearProgress />}
            {error && (
                <Alert severity="error" variant="outlined">
                    {error.response?.data.title ?? error.message}
                </Alert>
            )}
            <Box margin={2} display="flex" justifyContent="center">
                <Paper
                    sx={{
                        borderRadius: 2,
                        minWidth: '40ch',
                        width: '100%',
                        maxWidth: '120ch',
                        padding: '10px',
                    }}
                >
                    df
                </Paper>
            </Box>
        </>
    );
};
export default UsersControlPage;
