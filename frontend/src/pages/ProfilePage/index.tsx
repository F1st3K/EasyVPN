import { observer } from "mobx-react-lite";
import { FC, useContext } from "react";
import { Context } from "../..";

 
const ProfilePage: FC = () => {
    const { Auth } = useContext(Context);

    return ( 
    <>
        Привет человек {Auth.user.firstName} {Auth.roles[0]}
        <button onClick={() => Auth.logout()}>Выйти</button>
    </>
     );
}
 
export default observer(ProfilePage);