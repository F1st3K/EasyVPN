import { Alert, Box, LinearProgress, Paper } from '@mui/material';
import React, { useContext } from 'react';
import { FC } from 'react';
import { useLocation } from 'react-router-dom';

import { Context } from '../..';
import EasyVpn, { ApiError, Role } from '../../api';
import Page from '../../api/common/Page';
import { useRequest } from '../../hooks';
import MarkDownX from '../../modules/MarkDownX';

const DyncamicPages: FC = () => {
    const { Auth } = useContext(Context);
    const location = useLocation();
    const [data, loading, error] = useRequest<Page, ApiError>(
        () =>
            EasyVpn.pages
                .get('/'.concat(location.pathname.split('/').slice(2).join('/')))
                .then((r) => r.data),
        [location],
    );
    if (loading) return <LinearProgress />;

    return error ? (
        <Alert severity="error" variant="outlined">
            {error.response?.data.title ?? error.message}
        </Alert>
    ) : (
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
                <MarkDownX
                    editable={Auth.roles.includes(Role.PageModerator)}
                    onSave={(pr) => console.log(pr)}
                    mdInit={`---
title: ${data?.title}
route: ${data?.route}
modified: ${data?.lastModified}
created: ${data?.created}
---
${data?.base64Content}`}
                />
            </Paper>
        </Box>
    );
};
export default DyncamicPages;
