import {Threads} from "./Threads/Threads";
import {RightPanel} from "./RightPanel/RightPanel";
import {useLocalStorage} from "@uidotdev/usehooks";
import {useNavigate} from "react-router-dom";
import {useEffect} from "react";
import { jwtDecode } from "jwt-decode";
import axios from "axios";

export const Homepage = () => {
    const [JWT, setJWT] = useLocalStorage("JWT", "");
    const Navigator = useNavigate();

    useEffect(() => {
        let decodedToken = jwtDecode(JWT);

        let currentDate = new Date();

        if (decodedToken.exp! * 1000 < currentDate.getTime()) {
            Navigator("/login")
        }
        else{
            axios.defaults.headers.common['Authorization'] = `Bearer ${JWT}`;
        }
    }, [])

    return (
        <div className="w-full h-full flex justify-around">
            <div/>
            <Threads/>
            <RightPanel/>
        </div>
    )
}
