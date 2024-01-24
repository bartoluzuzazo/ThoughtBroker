import {PostThought} from "./PostThought/PostThought";
import {useEffect, useState} from "react";
import axios from "axios";
import {IUser, Profile} from "./Profile/Profile";
import {useLocalStorage} from "@uidotdev/usehooks";
import {useNavigate} from "react-router-dom";
import {jwtDecode} from "jwt-decode";
import {Guest} from "./Guest/Guest";

export const RightPanel = () => {

    const [user, setUser] = useState<IUser>();

    const [JWT, setJWT] = useLocalStorage("JWT", "");
    const Navigator = useNavigate();

    useEffect(() => {
        if(!JWT || JWT===""){
            setUser(undefined);
            return;
        }
        let decodedToken = jwtDecode(JWT);
        let currentDate = new Date();
        if (decodedToken.exp! * 1000 < currentDate.getTime()) {
            setUser(undefined);
        }
        else{
            axios.defaults.headers.common['Authorization'] = `Bearer ${JWT}`;
            axios.get("http://localhost:5102/api/User/claims").then(({data}) => {
                setUser(data);
            });
        }
    }, [])

    return (
        <div>
            {
                user?
                    <div>
                        <Profile user={user}/>
                        <PostThought/>
                    </div>
                :
                    <div>
                        <Guest/>
                    </div>
            }
        </div>
    )
}