import {BrowserRouter, Route, Routes} from "react-router-dom";
import {Homepage} from "../Homepage/Homepage";
import {Login} from "../Login/Login";


const DefaultRouter = () => {
    return (
        <BrowserRouter>
            <Routes>
                <Route path="/login" element={<Login/>}/>
                <Route path="/home" element={<Homepage/>} />
                <Route path="*" element={<Homepage/>} />
            </Routes>
        </BrowserRouter>
    );
};

export default DefaultRouter;