import { Box, AppBar, Toolbar, Typography, Button, IconButton, MenuItem } from "@mui/material";
import { FC, useContext, useState } from "react";
import { observer } from "mobx-react-lite";
import { Context } from "../..";
import { AccountCircle, AdminPanelSettings, SupportAgent, VpnKey } from "@mui/icons-material";
import { Role } from "../../api";
import PopUpMenu from "../../components/PopUpMenu";
import { useNavigate } from "react-router-dom";
 
const Header: FC = () => {
    const { Auth } = useContext(Context);
    const navigate = useNavigate();
    const Is = (role: Role) => Auth.roles.includes(role);

    const [anchorTickets, setAnchorTickets] = useState<null | HTMLElement>(null);
    const [anchorAdmin, setAnchorAdmin] = useState<null | HTMLElement>(null);

    return ( 
<Box sx={{ flexGrow: 1 }}>
    <AppBar>
        <Toolbar>
            <Button onClick={() => navigate("/")}
                size="large"
                color="inherit"
                sx={{ textTransform:"none" }}
            >
                <Typography fontSize="18pt" component="div">
                    EasyVPN
                </Typography>
            </Button>
            <Box sx={{ flexGrow: 1 }}>
                {Is(Role.Administrator) && <>
                <IconButton 
                    size="large"
                    color="inherit"
                    onClick={e => setAnchorAdmin(e.currentTarget)}
                >
                    <AdminPanelSettings/>
                </IconButton>
                <PopUpMenu 
                    anchorEl={anchorAdmin}
                    onClose={() => setAnchorAdmin(null)}
                >
                    <MenuItem>
                        Users
                    </MenuItem>
                    <MenuItem>
                        Connections
                    </MenuItem>
                    <MenuItem>
                        Servers
                    </MenuItem>
                </PopUpMenu>
                </>}
                {(Is(Role.PaymentReviewer) || Is(Role.Administrator)) && <>
                <IconButton
                    size="large"
                    color="inherit"
                    onClick={e => setAnchorTickets(e.currentTarget)}
                >
                    <SupportAgent/>
                </IconButton>
                <PopUpMenu 
                    anchorEl={anchorTickets}
                    onClose={() => setAnchorTickets(null)}
                >
                    {Is(Role.PaymentReviewer) && 
                    <MenuItem>
                        Connection Tickets
                    </MenuItem>}
                    {Is(Role.Administrator) && 
                    <MenuItem>
                        Support Tickets
                    </MenuItem>}
                </PopUpMenu>
                </>}
                {Auth.roles.includes(Role.Client) &&
                <Button
                    size="large"
                    color="inherit"
                    startIcon={<VpnKey/>}
                >
                    Connections
                </Button>
                }
            </Box>
            
            {Auth.isAuth ?
            <Button
                size="large"
                color="inherit"
                sx={{ textTransform:"none"}}
                endIcon={<AccountCircle/>}
            >
                <Box onClick={() => navigate("/profile")}
                    sx={{ flexDirection:"column", textAlign:"right" }}>
                    <Typography fontSize="14pt">
                        {Auth.user.firstName} {Auth.user.lastName}
                    </Typography >
                    <Typography fontSize="9pt">
                        {Auth.user.login}
                    </Typography>
                </Box>
            </Button>
            :   
            <Button onClick={() => navigate("/auth/login")} color="inherit">
                Sign In
            </Button>
            }
        </Toolbar>
    </AppBar>
    <Toolbar/>
</Box>
    );
}
 
export default observer(Header);