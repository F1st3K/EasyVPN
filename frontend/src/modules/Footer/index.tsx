import { Container, Typography, Link, Paper } from "@mui/material";
import { FC } from "react";

const Footer: FC = () => {
    return ( 
<Paper sx={{
        width: '100%',
        position: 'static',
        bottom: 0,
        borderBottom: 'none',
        borderLeft: 'none',
        borderRight: 'none',
    }} component={"footer"} square variant="outlined">
    
    <Container sx={{ p: 4 }} maxWidth="sm">
        <Typography variant="body2" color="text.secondary" align="center">
            {"Made by · "}
            <Link href="https://github.com/F1st3K" color="text.secondary" underline="hover">
                Nikita Kostin
            </Link>{" · "}
            <Link href="https://github.com/F1st3K/EasyVPN/graphs/contributors" color="text.secondary" underline="hover">
                Contributors
            </Link>{" · "}
            <Link href="https://github.com/F1st3K/EasyVPN" color="text.secondary" underline="hover">
                GitHub
            </Link> 
        </Typography>
        <Typography variant="body2" color="text.secondary" align="center">
            {"Copyright © EasyVPN "}
            {new Date().getFullYear()}
            {"."}
        </Typography>
    </Container>
</Paper>
     );
}
 
export default Footer;