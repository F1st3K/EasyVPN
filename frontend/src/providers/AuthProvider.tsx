import { LinearProgress } from "@mui/material";
import { HttpStatusCode } from "axios";
import { FC, ReactElement, useContext } from "react";
import { Context } from "..";
import { ApiError } from "../api";
import { useRequest } from "../hooks";
import { observer } from "mobx-react-lite";

interface AuthProviderProps {
    children?: ReactElement
}
 
const AuthProvider: FC<AuthProviderProps> = (props: AuthProviderProps) => {
    const store = useContext(Context)
    const [_, loading, error] = useRequest<void, ApiError>(
        () => store.Auth.checkAuth());

    if (loading)
        return (<LinearProgress />);
    
    if (error?.response?.data.status === HttpStatusCode.Unauthorized)
        store.Auth.logout();

    if (store.Auth.isAuth)
        return ( <>{props.children}</> );

    return ( <>{props.children}</> );
}
 
export default observer(AuthProvider);