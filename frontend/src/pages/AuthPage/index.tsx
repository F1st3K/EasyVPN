import { TabContext, TabPanel } from '@mui/lab';
import { Paper, Tab, Tabs } from '@mui/material';
import React, { FC } from 'react';
import { useNavigate } from 'react-router-dom';

import CenterBox from '../../components/CenterBox';
import LoginForm from '../../modules/LoginForm';
import RegisterForm from '../../modules/RegisterForm';

type AuthPageTabs = 'login' | 'register';

type AuthPageProps = {
    tab?: AuthPageTabs;
};

const AuthPage: FC<AuthPageProps> = (props: AuthPageProps) => {
    const navigate = useNavigate();

    const handleChangeTab = (_: unknown, newValue: string) => {
        navigate(`/auth/${newValue}`);
    };

    return (
        <CenterBox>
            <Paper
                elevation={3}
                sx={{
                    borderRadius: 2,
                    marginTop: '8vh',
                    marginBottom: '8vh',
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                }}
            >
                <TabContext value={props.tab ?? 'login'}>
                    <Tabs onChange={handleChangeTab} value={props.tab ?? 'login'} aria-label="basic tabs example">
                        <Tab label="Sign In" value={'login'} />
                        <Tab label="Sign Up" value={'register'} />
                    </Tabs>
                    <TabPanel value={'login'}>
                        <LoginForm />
                    </TabPanel>
                    <TabPanel value={'register'}>
                        <RegisterForm />
                    </TabPanel>
                </TabContext>
            </Paper>
        </CenterBox>
    );
};

export default AuthPage;
