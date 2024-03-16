import {Button, TextField} from "@mui/material";
import { useLocalStorage } from "@uidotdev/usehooks";
import axios from "axios";
import {useEffect, useState} from "react";
import {useNavigate} from "react-router-dom";
import {jwtDecode} from "jwt-decode";

export const Login = () => {
    const [validationText, setValidationText] = useState("")

    const [JWT, setJWT] = useLocalStorage("JWT", "");

    const [email, setEmail] = useState<string>("");
    const [password, setPassword] = useState<string>("");
    const Navigator = useNavigate();

    useEffect(() => {
        if(!JWT || JWT==="") return;

        let decodedToken = jwtDecode(JWT);
        let currentDate = new Date();
        if (!(decodedToken.exp! * 1000 < currentDate.getTime())) {
            axios.defaults.headers.common['Authorization'] = `Bearer ${JWT}`;
            axios.get("http://localhost:5102/api/User/claims").then(({data}) => {
                Navigator("/home");
            });
        }
    }, [])

    const handleLogin = () => {
        if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(email)) {
            setValidationText("Invalid email address")
            return;
        }
        const UserDTO = {
            email: email,
            password: password,
        }
        console.log(UserDTO);

        axios.post("http://localhost:5102/api/User/login", UserDTO).then((result) => {
            console.log(result.data["token"]);
            setJWT(result.data["token"]);
            axios.defaults.headers.common['Authorization'] = `Bearer ${JWT}`;
            Navigator("/home");
        }).catch((error) => {
            console.log(error);
        })
    }

    return (
        <div className="w-full h-full flex items-center justify-center">
            <div className="max-w-80">
                <TextField margin="normal" required fullWidth id="email" label="Email Address" name="email" autoComplete="email" autoFocus onChange={(e)=> setEmail(e.target.value)}/>
                <TextField margin="normal" required fullWidth name="password" label="Password" type="password" id="password" autoComplete="current-password" onChange={(e)=> setPassword(e.target.value)}/>

                <Button type="submit" fullWidth variant="contained" sx={{ mt: 3, mb: 2 }} onClick={handleLogin}>
                    Log in
                </Button>
                <Button type="submit" fullWidth color="error" variant="contained" sx={{ mt: 3, mb: 2 }} onClick={() => Navigator("/register")}>
                    Register Account
                </Button>
                <p>{validationText}</p>

            </div>
        </div>
    )
}
