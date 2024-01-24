import {Button, Card, CardActionArea, CardContent, Typography} from "@mui/material";
import {useNavigate} from "react-router-dom";

export const Guest = () => {

    const Navigator = useNavigate();

    const handleLogin = () => {
        Navigator("/login")
    }

    return (
        <div className="p-3">
            <CardActionArea component="a" disableTouchRipple={true}>
                <Card sx={{ display: 'flex' }}>
                    <CardContent sx={{ flex: 1 }}>
                        <Typography component="h2" variant="h5">
                            Guest
                        </Typography>
                        <Typography variant="subtitle1" color="text.secondary">
                            Log in to share your thoughts!
                        </Typography>
                        <Button variant="outlined" onClick={handleLogin}>
                            Log In
                        </Button>
                    </CardContent>
                </Card>
            </CardActionArea>
        </div>
    )
}