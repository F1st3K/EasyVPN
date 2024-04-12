import { Box, AppBar, Toolbar, IconButton, Typography, Button, Paper } from "@mui/material";
import { FC } from "react";
import { Role, User } from "../../api";

type NotAuth = null | undefined;
type Auth = { roles: Role[], user: User }

interface HeaderProps {
    auth?: Auth | NotAuth
}
 
const Header: FC<HeaderProps> = (props: HeaderProps) => {
    return ( 
<Box sx={{ flexGrow: 1 }}>
    <AppBar position="static">
        <Toolbar>
            <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                EasyVPN
            </Typography>
            
            
            {props.auth ?
            <Typography component="div">
                {props.auth.user.firstName} {props.auth.user.lastName}
            </Typography>
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
 
export default Header;