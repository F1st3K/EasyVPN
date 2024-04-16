import { FC } from "react";
import Header from "../../modules/Header";
import { Container } from "@mui/material";
import Footer from "../../modules/Footer";
import { Outlet } from "react-router-dom";

const Root: FC = () => {
    return ( 
<>
    <Header/>
        <Container component="main" maxWidth="xs" sx={{ 
            display:"flex",
            justifyContent:"center",
            alignItems: "center" }}
        >
            <Outlet/>
        </Container>
    <Footer/>
</>
     );
}
 
export default Root;