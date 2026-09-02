import './App.css'
import {Route, Routes} from 'react-router'
import DashboardPage from "./pages/Dashboard.tsx";
import LoginPage from "./pages/Login.tsx";
import RegisterPage from "./pages/Register.tsx";
import AuthLayout from "./layouts/AuthLayout.tsx";
import ApplicationLayout from "./layouts/ApplicationLayout.tsx";

export default function App() {

    return (
        <Routes>
            <Route element={<ApplicationLayout/>}>
                <Route index path="/" element={<DashboardPage/>}/>
            </Route>

            <Route element={<AuthLayout/>}>
                <Route path="login" element={<LoginPage/>}/>
                <Route path="register" element={<RegisterPage/>}/>
            </Route>
        </Routes>
    )
}
