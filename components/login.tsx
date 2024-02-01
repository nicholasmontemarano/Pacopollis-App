"use client";

import { useState } from "react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { redirect } from "next/navigation";
import axios from "axios";

export const LoginForm = () => {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [birthday, setBirthday] = useState("");
  const [ssn, setSSN] = useState("");
  const [password, setPassword] = useState("");
  const [user, setUser] = useState();
  const [ssn_state, set_ssn_state] = useState("valid");
  const [voterID, set_voter_id] = useState("");

  const handleSSN = (e: string) => {
    setSSN(e);
    let regex = /\d\d\d\d/i;
    if(!regex.test(e)){
        set_ssn_state("has_danger");
    }
  };

  const onSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    try {
      const url =
        "http://localhost:5014/api/Login?FirstName=" +
        firstName +
        "&LastName=" +
        lastName +
        "&SSN=" +
        ssn +
        "&Birthday=" +
        birthday +
        "&Password=" +
        password;
      const response = await axios
        .get(url)
        .then((response) => {
          if (response.data.userID != -1) {
            console.log(response.data);
            sessionStorage.setItem("user", response.data.userID);
            location.href = "http://localhost:3000/dashboard";
          } else {
            alert("Incorrect Information");
          }
        })
        .catch((err) => console.log(err));
    } catch (error) {
      console.error(error);
    }
  };

  return (
    <form onSubmit={onSubmit} className="space-y-8 w-[400px]">
      <div className="grid w-full max-w-sm items-center gap-1.5">
        <Label htmlFor="firstName">First Name</Label>
        <Input
          value={firstName}
          onChange={(e) => setFirstName(e.target.value)}
          id="firstName"
        />
      </div>
      <div className="grid w-full max-w-sm items-center gap-1.5">
        <Label htmlFor="lastName">Last Name</Label>
        <Input
          value={lastName}
          onChange={(e) => setLastName(e.target.value)}
          id="lastName"
        />
      </div>
      <div className="grid w-full max-w-sm items-center gap-1.5">
        <Label htmlFor="birthday">Birthday</Label>
        <Input
          value={birthday}
          onChange={(e) => setBirthday(e.target.value)}
          type="date"
          id="birthday"
        />
      </div>
      <div className="grid w-full max-w-sm items-center gap-1.5">
        <Label htmlFor="ssn">Last Four Digits of SSN</Label>
        <Input
          value={ssn}
          onChange={(e) => handleSSN(e.target.value)}
          type="password"
          id="ssn"
        />
      </div>
      <div className="grid w-full max-w-sm items-center gap-1.5">
        <Label htmlFor="password">Password</Label>
        <Input
          value={password}
          onChange={(e) => setPassword(e.target.value)}
          type="password"
          id="password"
        />
      </div>
      <div>
        <Button className="w-full" size="lg" color="">
          Submit
        </Button>
      </div>
    </form>
  );
};
