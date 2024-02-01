import { LoginForm } from "@/components/login"
import { NavBubble } from "./navbar"

export default function LoginPage(){
  return(
    <main className="h-full min-h-screen w-screen py-8 flex justify-center items-center bg-slate-100">
      <div className="shadow-xl p-4 bg-white rounded-xl">
        <h1 className="font-semibold text-3xl mb-8 mt-2"> Sign In </h1>
        <LoginForm/>
        <div className="flex justify-center items-center">
          {/* <NavBubble/> */}
        </div>
      </div>
    </main>
  )
}