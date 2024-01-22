import {Button, TextField} from "@mui/material";

export const Login = () => {

    return (
        <div className="w-full h-full flex items-center justify-center">
            <div className="max-w-80">
                <TextField margin="normal" required fullWidth id="email" label="Email Address" name="email" autoComplete="email" autoFocus/>
                <TextField margin="normal" required fullWidth name="password" label="Password" type="password" id="password" autoComplete="current-password"/>

                <Button type="submit" fullWidth variant="contained" sx={{ mt: 3, mb: 2 }}>
                    Log in
                </Button>
            </div>
        </div>
    )
}
