import {Button, TextField} from "@mui/material";
import SendIcon from '@mui/icons-material/Send';
import {useState} from "react";
import axios from "axios";
import {useNavigate} from "react-router-dom";

export const PostThought = () => {

    const [content, setContent] = useState("");
    const Navigator = useNavigate();

    const handlePost = () => {
        const ThoughtDTO = {
            content: content,
        }

        axios.post("http://localhost:5102/api/Thought", ThoughtDTO).then(() => {
            Navigator(0);
        }).catch((error) => {
            console.log(error);
        })
    }

    return (
        <div className="p-3 flex flex-col">
            <TextField
                id="outlined-multiline-flexible"
                label="Share your thoughts!"
                multiline
                rows={4}
                onChange={(e)=>setContent(e.target.value)}
            />
            <Button variant="contained" endIcon={<SendIcon />} onClick={handlePost}>
                Send
            </Button>
        </div>
    )
}