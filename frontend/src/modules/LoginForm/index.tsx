import { FC, useContext, useState } from "react";
import { Context } from "../..";
import { observer } from "mobx-react-lite";
import Button from '@mui/material/Button';
import TextField from "@mui/material/TextField";
import SecretFilledField from "../../components/SecretFilledField";
import "./style.css"
import { Box } from "@mui/material";
 
const LoginForm: FC = () => {
    const [login, setLogin] = useState<string>("")
    const [password, setPassword] = useState<string>("")
    const store = useContext(Context)

    return (  
<Box className="login-form">
    <TextField 
        label="Login" 
        variant="filled"
        onChange={e => setLogin(e.target.value)}
        value={login}
    />
    <SecretFilledField
        label="Password"
        onChange={e => setPassword(e.target.value)}
        value={password}
    />
    <Button 
        variant="contained" 
        size="large"
        onClick={() => store.Auth.login(login, password)}
    >
        Login
    </Button>
</Box>
    );
}
 
export default observer(LoginForm);
