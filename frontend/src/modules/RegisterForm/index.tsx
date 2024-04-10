import { FC, useContext, useState } from "react";
import { Context } from "../..";
import { observer } from "mobx-react-lite";
 
const RegisterForm: FC = () => {
    const [firstName, setFirstName] = useState<string>("")
    const [lastName, setLastName] = useState<string>("")
    const [login, setLogin] = useState<string>("")
    const [password, setPassword] = useState<string>("")
    const store = useContext(Context)

    return (  
        <>
            <input 
                onChange={e => setFirstName(e.target.value)}
                value={firstName}
                type="text"
                placeholder="first name" 
            />
            <input 
                onChange={e => setLastName(e.target.value)}
                value={lastName}
                type="text"
                placeholder="last name" 
            />
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
            <button onClick={() => store.Auth.register({
                firstName, lastName, login, password
            })}>
                Зарегистрироватся
            </button>
        </>
    );
}
 
export default observer(RegisterForm);
