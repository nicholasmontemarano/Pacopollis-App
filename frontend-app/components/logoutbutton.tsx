"use client"

import { Button } from "./ui/button";
import Link from "next/link";

export const Logout = () => {
    function logout(){
        sessionStorage.removeItem("user");
    }
    
    return (
        <Button onClick={logout} asChild>
            <Link href={"http://localhost:3000/"}>Log Out</Link>
        </Button>
    )
}
