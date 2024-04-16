import { FC, useState } from "react";
import LoginForm from "../../modules/LoginForm";
import RegisterForm from "../../modules/RegisterForm";
import { Box, Tab, Tabs } from "@mui/material";
import { TabContext, TabPanel } from "@mui/lab";
import { useNavigate } from "react-router-dom";
 
type AuthPageTabs = "login" | "register";

type AuthPageProps = {
    tab?: AuthPageTabs
}

const AuthPage: FC<AuthPageProps> = (props: AuthPageProps) => {
    const navigate = useNavigate();
    const [tab, setTab] = useState<AuthPageTabs>(props.tab ?? "login");
    
    const handleChangeTab = (_: any, newValue: any) => {
        setTab(newValue);
        navigate(`/auth/${newValue}`);
    };

    return ( 
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
            <Tabs onChange={handleChangeTab} value={tab} aria-label="basic tabs example">
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
     );
}
 
export default AuthPage;