import { useContext, useEffect } from "react";
import { Context } from ".";
import AuthPage from "./pages/AuthPage";
import { observer } from "mobx-react-lite";


function App() {
    const store = useContext(Context)

    useEffect(() => {
        store.Auth.checkAuth();
    }, [])

    if (store.Auth.isAuth === false)
        return (<AuthPage/>);

    return (
        <>
            Привет человек {store.Auth.user.firstName} {store.Auth.roles[0]}
            <button onClick={() => store.Auth.logout()}>Выйти</button>
        </>
    );
}

export default observer(App);
