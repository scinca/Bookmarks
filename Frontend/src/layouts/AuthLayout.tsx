import {Outlet} from "react-router";

export default function AuthLayout() {
    return (
        <>
            <header>Header</header>
            <Outlet/>
            <footer>
                Footer
            </footer>
        </>


    )
}