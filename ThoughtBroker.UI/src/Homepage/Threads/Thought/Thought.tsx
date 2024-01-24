import {FunctionComponent, useEffect, useState} from "react";
import {Button, Card, CardActionArea, CardContent, Typography} from "@mui/material";
import axios from "axios";

export interface IThought {
    id: string
    content: string
    timestamp: Date
    userId: string
    username: string
    parentId?: string
    endorsements: number
    condemnations: number
}

interface Props {
    thought: IThought
}

export const Thought: FunctionComponent<Props> = ({thought}) => {

    const [endorsements, setEndorsements] = useState(thought.endorsements)
    const [condemnations, setCondemnations] = useState(thought.condemnations)
    const [opinionExists, setOpinionExists] = useState(true)

    useEffect(() => {
        console.log(thought.id);
        axios.get("http://localhost:5102/api/Opinion/exists", {params: {"thoughtId": thought.id}}).then(({data}) => {
            setOpinionExists(data);
        });
    }, [])

    const handleOpinion = (isPositive: boolean) => {

        const OpinionDTO = {
            thoughtId: thought.id,
            isPositive: true
        }

        axios.post("http://localhost:5102/api/Opinion", OpinionDTO).then(() => {
            isPositive ? setEndorsements(endorsements + 1) : setCondemnations(condemnations + 1);
            setOpinionExists(true);
        }).catch((error) => {
            console.log(error);
        })
    }

    return (
        <div className="p-3">
            <CardActionArea component="a" href="#">
                <Card sx={{display: 'flex'}}>
                    <CardContent sx={{flex: 1}}>
                        <Typography component="h2" variant="h5">
                            {thought.username}
                        </Typography>
                        <Typography variant="subtitle1" color="text.secondary">
                            {thought.timestamp.toString()}
                        </Typography>
                        <Typography variant="body2" paragraph>
                            {thought.content}
                        </Typography>
                        <div className="flex justify-around">
                            <Typography variant="subtitle1" color="darkgreen" className="p-1">
                                Endorsements: {endorsements}
                            </Typography>
                            <Typography variant="subtitle1" color="darkred" className="p-1">
                                Condemnations: {condemnations}
                            </Typography>
                        </div>
                        {!opinionExists &&
                            <div className="flex justify-around">
                                <Button variant="contained" color="success" onClick={() => handleOpinion(true)}>
                                    Endorse!
                                </Button>
                                <Button variant="contained" color="error" onClick={() => handleOpinion(false)}>
                                    Condemn!
                                </Button>
                            </div>}
                    </CardContent>
                </Card>
            </CardActionArea>

        </div>
    )
}
