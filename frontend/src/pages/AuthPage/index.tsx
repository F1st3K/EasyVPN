import { FC, useState } from "react";
import LoginForm from "../../modules/LoginForm";
import RegisterForm from "../../modules/RegisterForm";
import Header from "../../modules/Header";
import Footer from "../../modules/Footer";
import { Container, Box, Tab, Tabs } from "@mui/material";
import { TabContext, TabPanel } from "@mui/lab";
 
interface AuthPageProps {
    tab?:"login" | "register"
}

const AuthPage: FC = (props: AuthPageProps) => {
    const [tab, setTab] = useState<"login" | "register">(props.tab ?? "login");

    return ( 
        <>
            <Header/>
            <Container component="main" maxWidth="xs" sx={{ display:"flex", justifyContent:"center", alignItems: "center" }}>
                <Box
                    sx={{
                    boxShadow: 3,
                    borderRadius: 2,
                    marginTop:"8vh",
                    marginBottom:"8vh",
                    display: "flex",
                    flexDirection: "column",
                    alignItems: "center",
                    }}
                >
                    <TabContext value={tab}>
                        <Tabs onChange={(e, v) => setTab(v)} value={tab} aria-label="basic tabs example">
                            <Tab label="Sign In" value={"login"} />
                            <Tab label="Sign Up" value={"register"} />
                        </Tabs>
                        <TabPanel value={"login"}>
                            <LoginForm/>
                        </TabPanel>
                        <TabPanel value={"register"}>
                            <RegisterForm/>
                        </TabPanel>
                    </TabContext>
                    
                </Box>
            </Container>
            <Footer/>
        </>
     );
}
 
export default AuthPage;