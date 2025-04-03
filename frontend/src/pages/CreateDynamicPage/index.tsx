import { Alert, Box, LinearProgress, Paper } from '@mui/material';
import React, { useContext, useEffect, useState } from 'react';
import { FC } from 'react';
import { useNavigate } from 'react-router-dom';

import { Context } from '../..';
import EasyVpn, { ApiError, Role } from '../../api';
import Page from '../../api/requests/Page';
import { useRequestHandler } from '../../hooks';
import MarkDownX from '../../modules/MarkDownX';

export function parseInput(data: string): Page {
    let route = '';
    let title = '';
    let content = '';
    let countBraces = 0;
    data.split('\n').map((line) => {
        if (line === '---') {
            countBraces++;
            return;
        }

        if (countBraces === 1 && line.startsWith('route:'))
            route = line.replace('route:', '').trim().replace(/^\/+/, '');
        if (countBraces === 1 && line.startsWith('title:'))
            title = line.replace('title:', '').trim();
        if (countBraces >= 2) content += line + '\n';
    });

    return { route, title, base64Content: content };
}

const CreateDynamicPage: FC = () => {
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
                    <MarkDownX
                        uniqKey={() => btoa('create')}
                        editable={Auth.roles.includes(Role.PageModerator)}
                        isEdit
                        onSave={(md) => {
                            const p = parseInput(md);
                            createHandler(p, async () => {
                                await Pages.sync();
                                navigate('../' + p.route);
                            });
                        }}
                        mdInit={`---
route: /new-route
title: Untitled
---
`}
                    />
                </Paper>
            </Box>
        </>
    );
};
export default CreateDynamicPage;
