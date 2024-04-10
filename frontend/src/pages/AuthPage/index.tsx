import { FC } from "react";
import LoginForm from "../../modules/LoginForm";
import RegisterForm from "../../modules/RegisterForm";
 
const AuthPage: FC = () => {
    return ( 
        <>
            <LoginForm/>
            <RegisterForm/>
        </>
     );
}
 
export default AuthPage;