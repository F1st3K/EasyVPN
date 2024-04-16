import { useContext} from "react";
import { Context } from ".";
import AuthPage from "./pages/AuthPage";
import { observer } from "mobx-react-lite";
import { useRequest } from "./hooks";
import { ApiError } from "./api";
import { HttpStatusCode } from "axios";
import { LinearProgress } from "@mui/material";
import "./App.css"
import Header from "./modules/Header";
import Footer from "./modules/Footer";
import RoutesProvider from "./providers/RoutesProvider";
import AuthProvider from "./providers/AuthProvider";


function App() {
    // const store = useContext(Context)
    // const [_, loading, error] = useRequest<void, ApiError>(
    //     () => store.Auth.checkAuth());

    // if (loading)
    //     return (<LinearProgress />);
    
    // if (error?.response?.data.status === HttpStatusCode.Unauthorized)
    //     store.Auth.logout();

    // if (store.Auth.isAuth === false)
    //     return (<AuthPage/>);

    return (
    <>
        <AuthProvider>
            <RoutesProvider/>
        </AuthProvider>
    </>
    );
}

export default App;
