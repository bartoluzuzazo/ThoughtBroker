import {useEffect, useState} from "react";
import {IThought, Thought} from "./Thought/Thought";
import axios from "axios";
import {Pagination, Skeleton} from "@mui/material";

export const Threads = () => {

    const quantity = 5;

    const [thoughts, setThoughts] = useState<IThought[]>();
    const [page, setPage] = useState(1);
    const [maxPages, setMaxPages] = useState(1);

    useEffect(() => {
        axios.get("http://localhost:5102/api/Thought/page", {params: {"page": 1, "quantity":quantity}}).then(({data}) => {
            setThoughts(data["thoughts"]);
            setMaxPages(data["pages"]);
            console.log(thoughts)
        });
    }, [])

    const handlePageChange = (e: any, v: number) => {
        axios.get("http://localhost:5102/api/Thought/page", {params: {"page": v, "quantity":quantity}}).then(({data}) => {
            setThoughts(data["thoughts"]);
            setMaxPages(data["pages"]);
            console.log(thoughts)
        });
        setPage(v);
    }

    return (
        <div className="border w-1/3">
            {thoughts?.map(t => <Thought thought={t}/>) ??
                <div className="p-3 h-80">
                    <Skeleton variant="rectangular"/>
                </div>}
            <Pagination count={maxPages} page={page} onChange={handlePageChange}/>
        </div>
    )
}