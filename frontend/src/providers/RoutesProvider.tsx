import { FC } from "react";
import { Navigate, Route, Routes } from "react-router-dom";
import Root from "../pages/Root";
import AuthPage from "../pages/AuthPage";
import ProfilePage from "../pages/ProfilePage";
import NotFoundPage from "../pages/NotFoundPage";
import MainPage from "../pages/MainPage";
 
const RoutesProvider: FC = () => {
    return ( 
    <Routes>
        <Route path="/" element={<Root/>}>
            <Route index element={<MainPage/>}/>
            <Route path="auth/">
                <Route index element={<Navigate to={"login"}/>}/>
                <Route path="login" element={<AuthPage tab="login"/>}/>
                <Route path="register" element={<AuthPage tab="register"/>}/>
            </Route>
            <Route path="profile" element={<ProfilePage/>}/>
            <Route path="*" element={<NotFoundPage/>}/>
        </Route>
    </Routes>
     );
}
 
export default RoutesProvider;