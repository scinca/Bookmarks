import {StrictMode} from 'react'
import {createRoot} from 'react-dom/client'
import {createBrowserRouter, RouterProvider} from "react-router"


import './index.css'
import App from './App.tsx'
import '@fontsource-variable/source-sans-3/wght.css';
import ApplicationLayout from "./layouts/ApplicationLayout.tsx";
import DashboardPage from "./pages/Dashboard.tsx";
import AuthLayout from "./layouts/AuthLayout.tsx";
import LoginPage from "./pages/Login.tsx";
import RegisterPage from "./pages/Register.tsx";
// useParams<{ id: number}>();
const router = createBrowserRouter([
    {
        path: "/",
        element: <ApplicationLayout/>,
        errorElement: <p>Not Found</p>,
        children: [
            {
                path: "/",
                element: <DashboardPage/>
            }
        ]
    },
    {
        path: "/",
        element: <AuthLayout/>,
        children: [
            {
                path: "login",
                element: <LoginPage/>
            },
            {
                path: "register",
                element: <RegisterPage/>
            }
        ]
    }
]);


createRoot(document.getElementById('root')!).render(
    <StrictMode>
        <RouterProvider router={router}/>
        <App/>
    </StrictMode>,
)