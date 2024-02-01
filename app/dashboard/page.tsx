"use client";

import { useEffect, useState } from "react";
import { Label } from "@radix-ui/react-label";
import { NavBubble } from "../navbar";
import { ElectionCard } from "@/components/electioncard";
import { useSearchParams } from "next/navigation";
import axios from "axios";
import Election from "@/app/interfaces/electionInterface";
import { ElectionList } from "@/components/electionlist";
import { Logout } from "@/components/logoutbutton";

export default function DashboardPage() {
  const [isBusy, setIsBusy] = useState(true);
  const [electionList, setElectionList] = useState();

  useEffect(() => {
    // Check for a user logon
    const electionData: Election[] = [];
    const loggedInUser = sessionStorage.getItem("user");
    if (loggedInUser == "-1" || loggedInUser == null || loggedInUser == undefined) {
      alert("No user logon");
      location.href = "http://localhost:3000/";
    } else {
      // Fill the electionData list with the list of elections
      try {
        const url =
          "http://localhost:5014/api/FindAllElections"
        const response = axios
          .get(url)
          .then((response) => {
            response.data.forEach(
              (electionItem: { electionId: number; startDate: any; endDate: any }) => {
                // add an instance to the ballotList
                const newElectionItem = {
                  id: electionItem.electionId,
                  electionName: electionItem.startDate.substring(0, 10),
                  voted: false,
                };
                electionData.push(newElectionItem);
              }
            )
            setElectionList(electionData);
            setIsBusy(false);
          })
          .catch((err) => console.log(err));
      } catch (error) {
        console.error(error);
      }
    }
  }, []);

  if (isBusy) {
    return (
      <Label>Loading...</Label>
    )
  } else {
    return (
      <main className="h-full w-screen min-h-screen flex px-24 justify-center items-top bg-slate-100">
        <div className="w-full shadow-xl p-4 bg-white rounded-xl">
          <h1 className="font-semibold text-3xl text-center py-4">
            Election Dashboard
          </h1>
          <ElectionList elections={electionList}></ElectionList>
          <div className="flex justify-center mt-4">
            {/* <NavBubble /> */}
            <Logout />
          </div>
        </div>
      </main>
    );
  }
}
