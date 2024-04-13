import { Box, AppBar, Toolbar, Typography, Button, IconButton } from "@mui/material";
import { FC, useContext } from "react";
import { observer } from "mobx-react-lite";
import { Context } from "../..";
import { AccountCircle } from "@mui/icons-material";
import { Role } from "../../api";

 
const Header: FC = () => {
    const { Auth } = useContext(Context);

    return ( 
<Box sx={{ flexGrow: 1 }}>
    <AppBar position="static">
        <Toolbar>
            <Typography variant="h6" component="div" sx={{m:"1ch"}}>
                EasyVPN
            </Typography>
            <Box sx={{ flexGrow: 1, textTransform:"none", display: { xs: 'none', md: 'flex' } }}>
                {Auth.roles.includes(Role.Administrator) && <>
                <Button
                    size="large"
                    color="inherit"
                >
                    Administrate
                </Button>
                <Button
                    size="large"
                    color="inherit"
                >
                    Support Tickets
                </Button>
                </>}
                {Auth.roles.includes(Role.PaymentReviewer) && 
                <Button
                    size="large"
                    color="inherit"
                >
                    Connection Tickets
                </Button>
                }
                {Auth.roles.includes(Role.Client) && 
                <Button
                    size="large"
                    color="inherit"
                >
                    My Connections
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
                <Box sx={{ flexDirection:"column", textAlign:"right" }}>
                    <Typography fontSize="14pt">
                        {Auth.user.firstName} {Auth.user.lastName}
                    </Typography >
                    <Typography fontSize="9pt">
                        {Auth.user.login}
                    </Typography>
                </Box>
            </Button>
            :   
            <Button color="inherit">
                Sign In
            </Button>
            }
        </Toolbar>
    </AppBar>
</Box>
    );
}
 
export default observer(Header);