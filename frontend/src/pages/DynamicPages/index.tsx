import { Alert, Box, LinearProgress, Paper } from '@mui/material';
import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import { FC } from 'react';
import { useLocation } from 'react-router-dom';

import { Context } from '../..';
import EasyVpn, { ApiError, Role } from '../../api';
import PageWithDates from '../../api/responses/PageWithDates';
import { useRequest } from '../../hooks';
import MarkDownX from '../../modules/MarkDownX';

const DyncamicPages: FC = () => {
    const { Auth } = useContext(Context);
    const location = useLocation();
    const [data, loading, error] = useRequest<PageWithDates, ApiError>(
        () =>
            EasyVpn.pages
                .get('/'.concat(location.pathname.split('/').slice(2).join('/')))
                .then((r) => r.data),
        [location.pathname],
    );
    if (loading || !data) return <LinearProgress />;

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
                `--- modified: ${data?.lastModified}
                created: ${data?.created}
                route: ${data?.route}
                title: ${data?.title}
                --- ${data?.base64Content}`{' '}
            </Paper>
        </Box>
    );
};
export default observer(DyncamicPages);
