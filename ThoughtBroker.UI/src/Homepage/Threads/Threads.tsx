import {useEffect, useState} from "react";
import {IThought, Thought} from "./Thought/Thought";
import axios from "axios";

export const Threads = () => {

    const [thoughts, setThoughts] = useState<IThought[]>();

    useEffect(() => {
        axios.get("http://localhost:5102/api/Thought/all").then(({data}) => {
            setThoughts(data["thoughts"]);
            console.log(thoughts)
        });
    }, [])

    return (
        <div className="border w-1/3">
            {thoughts?.map(t => <Thought thought={t}/>) ?? <p>nic</p>}
        </div>
    )
}