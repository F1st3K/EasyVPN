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
    const [login, setLogin] = useState<string>("")
    const [password, setPassword] = useState<string>("")
    const store = useContext(Context)

    const [loading, setLoading] = useState(false)
    const [error, setError] = useState<ApiError | null>(null)

    function loginHandler() {
        setLoading(true)
        store.Auth.login(login, password)
            .catch(e => {setError(e); console.log(e)})
            .finally(() => setLoading(false))
    }

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
    <LoadingButton 
        variant="contained" 
        size="large"
        onClick={loginHandler}
        loading={loading}
    >
        Login
    </LoadingButton>
    { error
    ? <Alert severity="error">{error.response?.data.title}</Alert>
    : null }
</Box>
    );
}
 
export default observer(LoginForm);
