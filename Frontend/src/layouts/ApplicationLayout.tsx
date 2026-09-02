import {Outlet} from "react-router";

export default function ApplicationLayout() {
    return (
        <>
            <header>Header</header>
            <Outlet/>
            <footer>Footer</footer>
        </>
    )
}