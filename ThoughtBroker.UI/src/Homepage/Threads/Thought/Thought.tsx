import {FunctionComponent} from "react";
import {Card, CardActionArea, CardContent, Typography} from "@mui/material";

export interface IThought {
    content: string
    timestamp: Date
    userId: string
    username: string
    parentId?: string
}

interface Props {
    thought: IThought
}

export const Thought : FunctionComponent<Props> = ({thought}) => {

    return (
        <div className="p-3">
            <CardActionArea component="a" href="#">
                <Card sx={{ display: 'flex' }}>
                    <CardContent sx={{ flex: 1 }}>
                        <Typography component="h2" variant="h5">
                            {thought.username}
                        </Typography>
                        <Typography variant="subtitle1" color="text.secondary">
                            {thought.timestamp.toString()}
                        </Typography>
                        <Typography variant="subtitle1" paragraph>
                            {thought.content}
                        </Typography>
                    </CardContent>
                </Card>
            </CardActionArea>

        </div>
    )
}
