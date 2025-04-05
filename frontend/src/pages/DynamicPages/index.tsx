import { Alert, Box, LinearProgress, Paper } from '@mui/material';
import { observer } from 'mobx-react-lite';
import React, { useContext } from 'react';
import { FC } from 'react';
import { useLocation, useNavigate } from 'react-router-dom';

import { Context } from '../..';
import EasyVpn, { ApiError, Role } from '../../api';
import Page from '../../api/requests/Page';
import PageWithDates from '../../api/responses/PageWithDates';
import { useRequest, useRequestHandler } from '../../hooks';
import MarkDownX from '../../modules/MarkDownX';
import { parseInput } from '../CreateDynamicPage';

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

    const navigate = useNavigate();
    const { Pages } = useContext(Context);

    const [updateHandler, loadingUp, errorUp] = useRequestHandler<void, ApiError, Page>(
        (page) =>
            EasyVpn.pages
                .update(data?.route ?? page.route, page, Auth.getToken())
                .then((r) => r.data),
    );
    const [removeHandler, loadingRm, errorRm] = useRequestHandler<void, ApiError>(() =>
        EasyVpn.pages.delete(data?.route ?? '', Auth.getToken()).then((r) => r.data),
    );

    if (loading) return <LinearProgress />;
    return error ? (
        <Alert severity="error" variant="outlined">
            {error.response?.data.title ?? error.message}
        </Alert>
    ) : (
        <Box margin={2} display="flex" justifyContent="center">
            {loadingUp && <LinearProgress />}
            {errorUp && (
                <Alert severity="error" variant="outlined">
                    {errorUp.response?.data.title ?? errorUp.message}
                </Alert>
            )}
            {loadingRm && <LinearProgress />}
            {errorRm && (
                <Alert severity="error" variant="outlined">
                    {errorRm.response?.data.title ?? errorRm.message}
                </Alert>
            )}
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
                    uniqKey={() => btoa(data?.route ?? '')}
                    editable={Auth.roles.includes(Role.PageModerator)}
                    onSave={(md) => {
                        const p = parseInput(md);
                        console.log(p);
                        updateHandler(p, async () => {
                            navigate('.');
                            await Pages.sync();
                            navigate('/pages/' + p.route);
                        });
                    }}
                    onDelete={() =>
                        removeHandler(null, async () => {
                            await Pages.sync();
                            navigate('.');
                        })
                    }
                    mdInit={`---
modified: ${data?.lastModified}
created: ${data?.created}
route: ${data?.route}
title: ${data?.title}
---
${data?.base64Content}`}
                />
            </Paper>
        </Box>
    );
};
export default observer(DyncamicPages);
