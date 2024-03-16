import {Button, TextField} from "@mui/material";
import { useLocalStorage } from "@uidotdev/usehooks";
import axios from "axios";
import {useState} from "react";
import {useNavigate} from "react-router-dom";

export const Register = () => {

    const [JWT, setJWT] = useLocalStorage("JWT", "");

    const [email, setEmail] = useState<string>("");
    const [username, setUsername] = useState<string>("");
    const [password, setPassword] = useState<string>("");
    const [rep_password, setRepPassword] = useState<string>("");
    const [validationText, setValidationText] = useState("")
    const Navigator = useNavigate();

    const handleRegister = () => {

        if(password!==rep_password) {
            setValidationText("Passwords are not the same")
            return;
        }
        if (!/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i.test(email)) {
            setValidationText("Invalid email address")
            return;
        }

        const RegisterDTO = {
            username: username,
            email: email,
            password: password
        }

        const UserDTO = {
            email: email,
            password: password,
        }

        axios.post("http://localhost:5102/api/User/register", RegisterDTO).then((result) => {
            axios.post("http://localhost:5102/api/User/login", UserDTO).then((result) => {
                console.log(result.data["token"]);
                setJWT(result.data["token"]);
                axios.defaults.headers.common['Authorization'] = `Bearer ${JWT}`;
                Navigator("/home");
            }).catch((error) => {
                console.log(error);
            })
        }).catch((error) => {
            if(error.response.status === 409){
                setValidationText("Username or email address is already taken")
            }
        })

    }

    return (
        <div className="w-full h-full flex items-center justify-center">
            <div className="max-w-80">
                <TextField margin="normal" required fullWidth id="email" label="Email Address" name="email" autoComplete="email" autoFocus onChange={(e)=> setEmail(e.target.value)}/>
                <TextField margin="normal" required fullWidth id="username" label="Username" name="username" autoComplete="username" autoFocus onChange={(e)=> setUsername(e.target.value)}/>
                <TextField margin="normal" required fullWidth name="password" label="Password" type="password" id="password" autoComplete="current-password" onChange={(e)=> setPassword(e.target.value)}/>
                <TextField margin="normal" required fullWidth name="reppassword" label="Repeat password" type="password" id="password" autoComplete="current-password" onChange={(e)=> setRepPassword(e.target.value)}/>

                <Button type="submit" fullWidth variant="contained" sx={{ mt: 3, mb: 2 }} onClick={handleRegister}>
                    Register
                </Button>
                <p>{validationText}</p>
            </div>
        </div>
    )
}
