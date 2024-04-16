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

interface ForProps {
    for: ReactElement
}

interface ForWithProps<T> extends ForProps {
    with?: T
}

const Unauth: FC<ForProps> = observer((props: ForProps) => {
    const { Auth } = useContext(Context);
    
    if (Auth.isAuth)
        return (<Navigate to={"/"}/>);
    
    return (<>{props.for}</>)
});

const Auth: FC<ForWithProps<Role>> = observer((props: ForWithProps<Role>) => {
    const { Auth } = useContext(Context);
    
    if (Auth.isAuth === false)
        return (<Navigate to={"/auth/login"}/>);

    if (props.with && Auth.roles.includes(props.with) === false)
        return (<NotFoundPage/>);
    
    return (<>{props.for}</>)
});
 
const RoutesProvider: FC = () => {
    return ( 
    <Routes>
        <Route path="/" element={<Root/>}>
            <Route index element={<MainPage/>}/>
            <Route path="auth/">
                <Route index element={<Navigate to={"login"}/>}/>
                <Route path="login" element={
                    <Unauth for={<AuthPage tab="login"/>}/>}/>
                <Route path="register" element={
                    <Unauth for={<AuthPage tab="register"/>}/>}/>
            </Route>
            <Route path="profile" element={
                <Auth for={<ProfilePage/>}/>}/>
            <Route path="*" element={<NotFoundPage/>}/>
        </Route>
    </Routes>
     );
}
 
export default RoutesProvider;