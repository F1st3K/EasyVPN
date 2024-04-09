import { FC, useContext, useState } from "react";
import { Context } from "../..";
import { observer } from "mobx-react-lite";
 
const LoginForm: FC = () => {
    const [login, setLogin] = useState<string>("")
    const [password, setPassword] = useState<string>("")
    const store = useContext(Context)

    return (  
        <>
            <input 
                onChange={e => setLogin(e.target.value)}
                value={login}
                type="text"
                placeholder="login" 
            />
            <input 
                onChange={e => setPassword(e.target.value)}
                value={password}
                type="password"
                placeholder="password" 
            />
            <button onClick={() => store.Auth.login(login, password)}>Войти</button>
        </>
    );
}
 
export default observer(LoginForm);
