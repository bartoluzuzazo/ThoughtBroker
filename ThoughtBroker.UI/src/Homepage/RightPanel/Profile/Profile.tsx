import {Button, Card, CardActionArea, CardContent, Typography} from "@mui/material";
import {FunctionComponent, useEffect} from "react";
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
    const Navigator = useNavigate();

    const handleLogout = () => {
        setJWT("");
        Navigator(0);
        console.log("a")
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
                        <Button variant="outlined" color="error" onClick={handleLogout}>
                            Log Out
                        </Button>
                    </CardContent>
                </Card>
            </CardActionArea>
        </div>
    )
}