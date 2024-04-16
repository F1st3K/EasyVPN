import { FC, useContext, ReactElement } from "react";
import { Navigate, Route, Routes} from "react-router-dom";
import Root from "../pages/Root";
import AuthPage from "../pages/AuthPage";
import ProfilePage from "../pages/ProfilePage";
import NotFoundPage from "../pages/NotFoundPage";
import MainPage from "../pages/MainPage";
import { Context } from "..";
import { observer } from "mobx-react-lite";
import { Role } from "../api";

interface OnProps {
    children?: ReactElement
}

interface OnAuthProps extends OnProps {
    role?: Role
}

const OnUnauth: FC<OnProps> = observer((props: OnProps) => {
    const { Auth } = useContext(Context);
    
    if (Auth.isAuth)
        return (<Navigate to={"/"}/>);
    
    return (<>{props.children}</>)
});

const OnAuth: FC<OnAuthProps> = observer((props: OnAuthProps) => {
    const { Auth } = useContext(Context);
    
    if (Auth.isAuth === false)
        return (<Navigate to={"/auth/login"}/>);

    if (props.role && Auth.roles.includes(props.role) === false)
        return (<NotFoundPage/>);
    
    return (<>{props.children}</>)
});
 
const RoutesProvider: FC = () => {
    return ( 
    <Routes>
        <Route path="/" element={<Root/>}>
            <Route index element={<MainPage/>}/>
            <Route path="auth/">
                <Route index element={<Navigate to={"login"}/>}/>
                <Route path="login" element={
                    <OnUnauth><AuthPage tab="login"/></OnUnauth>
                }/>
                <Route path="register" element={
                    <OnUnauth><AuthPage tab="register"/></OnUnauth>
                }/>
            </Route>
            <Route path="profile" element={
                <OnAuth><ProfilePage/></OnAuth>
            }/>
            <Route path="*" element={<NotFoundPage/>}/>
        </Route>
    </Routes>
     );
}
 
export default RoutesProvider;