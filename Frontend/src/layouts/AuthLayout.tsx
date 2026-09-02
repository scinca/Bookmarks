import {Outlet} from "react-router";
import "./AuthLayout.module.css"

export default function AuthLayout() {
    return (
        <>
            <header>Header</header>
            <main>
                <Outlet/>
            </main>
            <footer>
                Footer
            </footer>
        </>
    )
}