import {Button, TextField} from "@mui/material";
import { useLocalStorage } from "@uidotdev/usehooks";
import axios from "axios";
import {useState} from "react";
import {useNavigate} from "react-router-dom";

export const Login = () => {

    const [JWT, setJWT] = useLocalStorage("JWT", "");

    const [email, setEmail] = useState<string>("");
    const [password, setPassword] = useState<string>("");
    const Navigator = useNavigate();

    const handleLogin = () => {
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
            </div>
        </div>
    )
}
