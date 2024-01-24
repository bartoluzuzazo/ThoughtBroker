import {Threads} from "./Threads/Threads";
import {RightPanel} from "./RightPanel/RightPanel";
import {useLocalStorage} from "@uidotdev/usehooks";
import {useNavigate} from "react-router-dom";
import {useEffect} from "react";
import { jwtDecode } from "jwt-decode";
import axios from "axios";

export const Homepage = () => {

    return (
        <div className="w-full h-full flex justify-around">
            <div/>
            <Threads/>
            <RightPanel/>
        </div>
    )
}
