import {Threads} from "./Threads/Threads";
import {RightPanel} from "./RightPanel/RightPanel";

export const Homepage = () => {

    return (
        <div className="w-full h-full flex justify-around">
            <div/>
            <Threads/>
            <RightPanel/>
        </div>
    )
}
