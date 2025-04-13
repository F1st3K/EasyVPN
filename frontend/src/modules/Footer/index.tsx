import { Container, Link, Paper, Typography } from '@mui/material';
import React, { FC } from 'react';

import Package from '../../../package.json';

const Footer: FC = () => {
    return (
        <Paper
            sx={{
                width: '100%',
                position: 'static',
                bottom: 0,
                borderBottom: 'none',
                borderLeft: 'none',
                borderRight: 'none',
            }}
            component={'footer'}
            square
            variant="outlined"
        >
            <Container sx={{ p: 4 }} maxWidth="sm">
                <Typography variant="body2" color="text.secondary" align="center">
                    {'Made by · '}
                    <Link
                        href="https://github.com/F1st3K"
                        color="text.secondary"
                        underline="hover"
                    >
                        Nikita Kostin
                    </Link>
                    {' · '}
                    <Link
                        href="https://github.com/F1st3K/EasyVPN/graphs/contributors"
                        color="text.secondary"
                        underline="hover"
                    >
                        Contributors
                    </Link>
                    {' · '}
                    <Link
                        href="https://github.com/F1st3K/EasyVPN"
                        color="text.secondary"
                        underline="hover"
                    >
                        GitHub
                    </Link>
                </Typography>
                <Typography variant="body2" color="text.secondary" align="center">
                    <Link
                        href="https://creativecommons.org/licenses/by-nc/4.0/
"
                        color="text.secondary"
                        underline="hover"
                    >
                        {' Copyright © '}
                        {new Date().getFullYear()}
                        {' EasyVPN '}
                        {Package.version}
                    </Link>
                </Typography>
            </Container>
        </Paper>
    );
};

export default Footer;
