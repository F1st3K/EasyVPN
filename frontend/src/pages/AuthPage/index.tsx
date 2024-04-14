import { FC } from "react";
import LoginForm from "../../modules/LoginForm";
import RegisterForm from "../../modules/RegisterForm";
import Header from "../../modules/Header";
 
const AuthPage: FC = () => {
    return ( 
        <>
            <Header/>
            <LoginForm/>
            <RegisterForm/>
            <RegisterForm/>
            <RegisterForm/>
        </>
     );
}
 
export default AuthPage;