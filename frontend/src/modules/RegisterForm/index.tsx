import { FC, useContext, useState } from "react";
import { Context } from "../..";
import { observer } from "mobx-react-lite";
import { LoadingButton } from "@mui/lab";
import { Box, TextField, Alert } from "@mui/material";
import SecretFilledField from "../../components/SecretFilledField";
import { ApiError } from "../../api";
import "./style.css"
 
const RegisterForm: FC = () => {
    const [firstName, setFirstName] = useState<string>("")
    const [lastName, setLastName] = useState<string>("")
    const [login, setLogin] = useState<string>("")
    const [password, setPassword] = useState<string>("")
    const [remPassword, setRemPassword] = useState<string>("")
    const store = useContext(Context)

    const [loading, setLoading] = useState(false)
    const [error, setError] = useState<ApiError | null>(null)

    function registerHandler() {
        setLoading(true)
        store.Auth.login(login, password)
            .catch(e => {setError(e); console.log(e)})
            .finally(() => setLoading(false))
    }

    return (  
       
<Box className="register-form">
    <TextField sx={{width: "25ch"}}
        label="First name" 
        variant="filled"
        onChange={e => setFirstName(e.target.value)}
        value={firstName}
    />
    <TextField sx={{width: "25ch"}}
        label="Last name" 
        variant="filled"
        onChange={e => setLastName(e.target.value)}
        value={lastName}
    />
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
    <SecretFilledField sx={{width: "25ch"}}
        error={remPassword !== password}
        label="Remember password"
        onChange={e => setRemPassword(e.target.value)}
        value={remPassword}
    />
    <LoadingButton
        disabled={remPassword !== password}
        variant="contained" 
        size="large"
        onClick={registerHandler}
        loading={loading}
    >
        Sign Up
    </LoadingButton>
    {error? 
        <Alert severity="error" sx={{width: "25ch"}}>
            {error.response?.data.title ?? error.message}
        </Alert>
    :null}
</Box>
    );
}
 
export default observer(RegisterForm);
