import {
    AccountCircle,
    AdminPanelSettings,
    SupportAgent,
    VpnKey,
} from '@mui/icons-material';
import {
    AppBar,
    Box,
    Button,
    IconButton,
    MenuItem,
    Toolbar,
    Typography,
} from '@mui/material';
import { observer } from 'mobx-react-lite';
import React, { FC, useContext, useState } from 'react';
import { useNavigate } from 'react-router-dom';

import { Context } from '../..';
import { Role } from '../../api';
import Logo from '../../components/Logo';
import PopUpMenu from '../../components/PopUpMenu';

const Header: FC = () => {
    const { Auth } = useContext(Context);
    const navigate = useNavigate();
    const Is = (role: Role) => Auth.roles.includes(role);

    const [anchorTickets, setAnchorTickets] = useState<null | HTMLElement>(null);
    const [anchorAdmin, setAnchorAdmin] = useState<null | HTMLElement>(null);

    return (
        <AppBar position="sticky">
            <Toolbar>
                <Button
                    onClick={() => navigate('/')}
                    size="large"
                    color="inherit"
                    sx={{ textTransform: 'none', paddingLeft: 0 }}
                >
                    <Box
                        display="flex"
                        flexDirection="row"
                        flexWrap="wrap"
                        justifyContent="space-around"
                    >
                        <Logo theme="dark" marginX={2} size={40} />
                        <Typography fontFamily="mono" fontSize="18pt" component="div">
                            EasyVPN
                        </Typography>
                    </Box>
                </Button>
                <Box sx={{ flexGrow: 1 }}>
                    {Is(Role.Administrator) && (
                        <>
                            <IconButton
                                size="large"
                                color="inherit"
                                onClick={(e) => setAnchorAdmin(e.currentTarget)}
                            >
                                <AdminPanelSettings />
                            </IconButton>
                            <PopUpMenu
                                anchorEl={anchorAdmin}
                                onClose={() => setAnchorAdmin(null)}
                            >
                                <MenuItem
                                    onClick={() => {
                                        navigate('/control/connections');
                                        setAnchorAdmin(null);
                                    }}
                                >
                                    Connections
                                </MenuItem>
                                <MenuItem
                                    onClick={() => {
                                        navigate('/control/users');
                                        setAnchorAdmin(null);
                                    }}
                                >
                                    Users
                                </MenuItem>
                                <MenuItem
                                    onClick={() => {
                                        navigate('/control/servers');
                                        setAnchorAdmin(null);
                                    }}
                                >
                                    Servers
                                </MenuItem>
                            </PopUpMenu>
                        </>
                    )}
                    {(Is(Role.PaymentReviewer) || Is(Role.Administrator)) && (
                        <>
                            <IconButton
                                size="large"
                                color="inherit"
                                onClick={(e) => setAnchorTickets(e.currentTarget)}
                            >
                                <SupportAgent />
                            </IconButton>
                            <PopUpMenu
                                anchorEl={anchorTickets}
                                onClose={() => setAnchorTickets(null)}
                            >
                                {Is(Role.Administrator) && (
                                    <MenuItem
                                        onClick={() => {
                                            navigate('/tickets/support');
                                            setAnchorTickets(null);
                                        }}
                                    >
                                        Support Tickets
                                    </MenuItem>
                                )}
                                {Is(Role.PaymentReviewer) && (
                                    <MenuItem
                                        onClick={() => {
                                            navigate('/tickets/payment');
                                            setAnchorTickets(null);
                                        }}
                                    >
                                        Payment Tickets
                                    </MenuItem>
                                )}
                            </PopUpMenu>
                        </>
                    )}
                    {Auth.roles.includes(Role.Client) && (
                        <Button
                            size="large"
                            color="inherit"
                            startIcon={<VpnKey />}
                            onClick={() => navigate('/connections')}
                        >
                            Connections
                        </Button>
                    )}
                </Box>

                {Auth.isAuth ? (
                    <Button
                        onClick={() => navigate('/profile')}
                        size="large"
                        color="inherit"
                        sx={{ textTransform: 'none' }}
                        endIcon={<AccountCircle />}
                    >
                        <Box sx={{ flexDirection: 'column', textAlign: 'right' }}>
                            <Typography fontSize="14pt">
                                {Auth.user.firstName} {Auth.user.lastName}
                            </Typography>
                            <Typography fontSize="9pt">{Auth.user.login}</Typography>
                        </Box>
                    </Button>
                ) : (
                    <Button onClick={() => navigate('/auth')} color="inherit">
                        Sign In
                    </Button>
                )}
            </Toolbar>
        </AppBar>
    );
};

export default observer(Header);
