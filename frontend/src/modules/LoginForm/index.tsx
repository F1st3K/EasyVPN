import { FC, useContext, useState } from "react";
import { Context } from "../..";
import { observer } from "mobx-react-lite";
import TextField from "@mui/material/TextField";
import SecretFilledField from "../../components/SecretFilledField";
import "./style.css"
import { Alert, Box } from "@mui/material";
import { ApiError } from "../../api";
import LoadingButton from "@mui/lab/LoadingButton";
 
const LoginForm: FC = () => {
    const { Auth } = useContext(Context)
    
    const [login, setLogin] = useState<string>("")
    const [password, setPassword] = useState<string>("")

    const [loading, setLoading] = useState(false)
    const [error, setError] = useState<ApiError | null>(null)

    function loginHandler() {
        setLoading(true)
        Auth.login(login, password)
            .catch(e => {setError(e); console.log(e)})
            .finally(() => setLoading(false))
    }

    return (  
<Box className="login-form">
    <TextField sx={{width: "25ch"}}
        label="Login" 
        variant="filled"
        onChange={e => setLogin(e.target.value)}
        value={login}
    />
    <SecretFilledField sx={{width: "25ch"}}
        label="Password"
        onChange={e => setPassword(e.target.value)}
        value={password}
    />
    <LoadingButton 
        variant="contained" 
        size="large"
        onClick={loginHandler}
        loading={loading}
    >
        Sign In
    </LoadingButton>
    {error? 
        <Alert severity="error" sx={{width: "25ch"}}>
            {error.response?.data.title ?? error.message}
        </Alert>
    :null}
</Box>
    );
}
 
export default observer(LoginForm);
