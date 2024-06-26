import { FC } from "react";
import LoginForm from "../../modules/LoginForm";
import RegisterForm from "../../modules/RegisterForm";
import { Paper, Tab, Tabs } from "@mui/material";
import { TabContext, TabPanel } from "@mui/lab";
import { useNavigate } from "react-router-dom";
 
type AuthPageTabs = "login" | "register";

type AuthPageProps = {
    tab?: AuthPageTabs
}

const AuthPage: FC<AuthPageProps> = (props: AuthPageProps) => {
    const navigate = useNavigate();
    
    const handleChangeTab = (_: any, newValue: any) => {
        navigate(`/auth/${newValue}`);
    };

    return ( 
    <Paper elevation={3}
        sx={{
        borderRadius: 2,
        marginTop:"8vh",
        marginBottom:"8vh",
        display: "flex",
        flexDirection: "column",
        alignItems: "center",
        }}
    >
        <TabContext value={props.tab ?? "login"}>
            <Tabs onChange={handleChangeTab} value={props.tab ?? "login"} aria-label="basic tabs example">
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
        
    </Paper>
     );
}
 
export default AuthPage;