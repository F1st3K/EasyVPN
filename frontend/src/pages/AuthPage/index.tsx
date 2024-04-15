import { FC } from "react";
import LoginForm from "../../modules/LoginForm";
import RegisterForm from "../../modules/RegisterForm";
import Header from "../../modules/Header";
import Footer from "../../modules/Footer";
 
const AuthPage: FC = () => {
    return ( 
        <>
            <Header/>
            <LoginForm/>
            <RegisterForm/>
            <RegisterForm/>
            <RegisterForm/>
            <Footer/>
        </>
     );
}
 
export default AuthPage;