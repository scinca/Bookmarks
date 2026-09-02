import './App.css'
import {Route, Routes} from 'react-router'
import DashboardPage from "./pages/Dashboard.tsx";
import LoginPage from "./pages/Login.tsx";
import RegisterPage from "./pages/Register.tsx";
import AuthLayout from "./layouts/AuthLayout.tsx";

export default function App() {

    return (
        <Routes>
            <Route index path="/" element={<DashboardPage/>}/>

            <Route element={<AuthLayout/>}>
                <Route path="login" element={<LoginPage/>}/>
                <Route path="register" element={<RegisterPage/>}/>
            </Route>
        </Routes>
    )
}
