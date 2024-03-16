import {Button, Card, CardActionArea, CardContent, TextField, Typography} from "@mui/material";
import {FunctionComponent, useEffect, useState} from "react";
import axios from "axios";
import {useLocalStorage} from "@uidotdev/usehooks";
import {useNavigate} from "react-router-dom";

export interface IUser {
    id: string
    username: string
    role: string
}

interface Props {
    user: IUser
}

export const Profile : FunctionComponent<Props> = ({user}) => {

    const [JWT, setJWT] = useLocalStorage("JWT", "");
    const [pass, setPass] = useState("");
    const [newpass, setNewpass] = useState("");
    const Navigator = useNavigate();

    useEffect(() => {
        axios.defaults.headers.common['Authorization'] = `Bearer ${JWT}`;
    }, []);

    const handleLogout = (destination: any) => {
        setJWT("");
        Navigator(destination);
        console.log("a")
    }

    const handleChangePassword = () => {
        console.log(pass);
        if (pass !== newpass) return;
        axios.put("http://localhost:5102/api/User/password", {"password": pass}).then(({data}) => {
            handleLogout("/login");
        });
    }

    const handleDeleteAccount = () => {
        // eslint-disable-next-line no-restricted-globals
        var decision = confirm("Are you sure? This action cannot be reversed");
        if(decision){
            axios.delete("http://localhost:5102/api/User").then(({data}) => {
                handleLogout("/login");
            });
        }
    }

    return (
        <div className="p-3">
            <CardActionArea component="a" disableTouchRipple={true}>
                <Card sx={{ display: 'flex' }}>
                    <CardContent sx={{ flex: 1 }}>
                        <Typography component="h2" variant="h5">
                            {user.username}
                        </Typography>
                        <Typography variant="subtitle1" color="text.secondary">
                            {user.id}
                        </Typography>
                        <div className="flex flex-col">
                            <Button variant="outlined" color="error" onClick={(e)=>handleLogout(0)}>
                                Log Out
                            </Button>
                            <TextField margin="normal" type="password" required fullWidth id="pass" label="New password" name="pass" autoComplete="pass" autoFocus onChange={(e)=> setPass(e.target.value)}/>
                            <TextField margin="normal" type="password" required fullWidth id="newpass" label="Repeat password" name="newpass" autoComplete="newpass" autoFocus onChange={(e)=> setNewpass(e.target.value)}/>
                            <Button variant="outlined" color="error" onClick={handleChangePassword}>
                                Change Password
                            </Button>
                            <Button variant="outlined" color="error" onClick={handleDeleteAccount}>
                                DELETE ACCOUNT
                            </Button>
                        </div>
                    </CardContent>
                </Card>
            </CardActionArea>
        </div>
    )
}