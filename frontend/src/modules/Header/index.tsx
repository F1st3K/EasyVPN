import { AdminPanelSettings, SupportAgent, VpnKey } from '@mui/icons-material';
import {
    AppBar,
    Box,
    Button,
    CssBaseline,
    IconButton,
    MenuItem,
    Toolbar,
    Typography,
} from '@mui/material';
import { observer } from 'mobx-react-lite';
import React, { useContext, useState } from 'react';
import { useNavigate } from 'react-router-dom';

import { Context } from '../..';
import { Role } from '../../api';
import Logo from '../../components/Logo';
import PopUpMenu from '../../components/PopUpMenu';
import HeaderSpace from './HeaderSpase';

const Header = (props: { isMobile: () => boolean; toggleNav: () => void }) => {
    const { Auth } = useContext(Context);
    const navigate = useNavigate();
    const Is = (role: Role) => Auth.roles.includes(role);

    const handleMenu = () => {
        if (!props.isMobile()) {
            props.toggleNav();
            navigate('/');
        } else props.toggleNav();
    };

    const [anchorTickets, setAnchorTickets] = useState<null | HTMLElement>(null);
    const [anchorAdmin, setAnchorAdmin] = useState<null | HTMLElement>(null);

    return (
        <Box>
            <CssBaseline />
            <AppBar position="fixed" sx={{ zIndex: (theme) => theme.zIndex.drawer + 1 }}>
                <Toolbar>
                    <Button
                        onClick={handleMenu}
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
                            <Typography
                                marginTop={0.5}
                                fontFamily="mono"
                                fontSize="18pt"
                                component="div"
                            >
                                EasyVPN
                            </Typography>
                        </Box>
                    </Button>
                    <Box sx={{ flexGrow: 1 }}>
                        {(Is(Role.ConnectionRegulator) ||
                            Is(Role.SecurityKeeper) ||
                            Is(Role.ServerSetuper)) && (
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
                                    {Is(Role.ConnectionRegulator) && (
                                        <MenuItem
                                            onClick={() => {
                                                navigate('/control/connections');
                                                setAnchorAdmin(null);
                                            }}
                                        >
                                            Connections
                                        </MenuItem>
                                    )}
                                    {Is(Role.SecurityKeeper) && (
                                        <MenuItem
                                            onClick={() => {
                                                navigate('/control/users');
                                                setAnchorAdmin(null);
                                            }}
                                        >
                                            Users
                                        </MenuItem>
                                    )}
                                    {Is(Role.ServerSetuper) && (
                                        <>
                                            <MenuItem
                                                onClick={() => {
                                                    navigate('/control/servers');
                                                    setAnchorAdmin(null);
                                                }}
                                            >
                                                Servers
                                            </MenuItem>
                                            <MenuItem
                                                onClick={() => {
                                                    navigate('/control/protocols');
                                                    setAnchorAdmin(null);
                                                }}
                                            >
                                                Protocols
                                            </MenuItem>
                                        </>
                                    )}
                                </PopUpMenu>
                            </>
                        )}
                        {Is(Role.PaymentReviewer) && (
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
                        >
                            <Box sx={{ flexDirection: 'column', textAlign: 'right' }}>
                                <Typography fontSize="14pt">
                                    {Auth.user.firstName} {Auth.user.lastName}
                                </Typography>
                                <Typography fontSize="9pt">{Auth.user.login}</Typography>
                            </Box>
                            <Box
                                marginLeft={3}
                                component="img"
                                src={Auth.user.icon}
                                sx={{
                                    width: '5ch',
                                    height: '5ch',
                                    borderRadius: '50%',
                                    objectFit: 'cover',
                                }}
                            />
                        </Button>
                    ) : (
                        <Button onClick={() => navigate('/auth')} color="inherit">
                            Sign In
                        </Button>
                    )}
                </Toolbar>
            </AppBar>
        </Box>
    );
};

export default observer(Header);
export { HeaderSpace };
